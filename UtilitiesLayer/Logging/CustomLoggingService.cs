using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using UtilitiesLayer.Configuration;
using UtilitiesLayer.Models;
using static UtilitiesLayer.Models.LogTypes;

namespace UtilitiesLayer.Logging
{
    public static class CustomLoggingService
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static void Log(LogType type, Exception ex, string debugMessage)
        {
            var context = _httpContextAccessor?.HttpContext;
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            Type declaringType = method.DeclaringType;
            string methodName = method.Name;
            if (method.IsConstructor)
            {
                methodName = "Constructor";
            }
            string exceptionType = ex.GetType().Name;
            string exceptionMessage = ex.InnerException?.Message ?? "";
            var connectionString = ConfigurationUtility.GetSetting("AMSConnectionString");
            if (IsDatabaseAvailable(connectionString))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string LogInsertSQL = "insert into Logs (Exception,ClassName,MethodName,Message,TimeStamp,Ip) values(@Exception,@ClassName,@MethodName,@Message,@TimeStamp,@Ip)";
                    using (SqlCommand command = new SqlCommand(LogInsertSQL, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@Exception", exceptionType);
                        command.Parameters.AddWithValue("@ClassName", declaringType.FullName);
                        command.Parameters.AddWithValue("@MethodName", methodName);
                        command.Parameters.AddWithValue("@Message", exceptionMessage);
                        command.Parameters.AddWithValue("@TimeStamp", DateTime.Now);
                        command.Parameters.AddWithValue("@DebugMessage", debugMessage);
                        command.Parameters.AddWithValue("@Ip", context.Connection.RemoteIpAddress?.ToString());

                        // Execute the query
                        if(connection.State == System.Data.ConnectionState.Closed)
                            connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                    }
                }
            }
            else
            {
                string filePath = "Logs/log-" + DateTime.Now.ToString("MMMM") + ".json";
                string directoryPath = "Logs";
                string dateKey = DateTime.Now.ToString("dd-MM-yy");  // Format date as "dd-mm-yy"
                string timeKey = DateTime.Now.ToString("HH-00");
                Directory.CreateDirectory(directoryPath);
                if (!File.Exists(filePath))
                {
                    // If the file doesn't exist, create it and add an empty object
                    File.WriteAllText(filePath, "{}");
                }

                string json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, LogEntry>>>>(json);  // Deserialize to a nested dictionary

                // Check if the date key exists, if not, add it
                if (!data.ContainsKey(dateKey))
                {
                    data[dateKey] = new Dictionary<string, Dictionary<string, LogEntry>>();
                }

                // Check if the time key exists, if not, add it
                if (!data[dateKey].ContainsKey(timeKey))
                {
                    data[dateKey][timeKey] = new Dictionary<string, LogEntry>();
                }

                int index = data[dateKey][timeKey].Count;
                index++;
                var logEntry = new LogEntry
                {
                    LogType = "error",
                    Exception = exceptionType,
                    ClassName = declaringType.FullName,
                    MethodName = methodName,
                    DebugMessage = debugMessage,
                    Message = exceptionMessage,
                    TimeStamp = DateTime.Now,
                    EndPoint = context.Request.Path,
                    Ip = context.Connection.RemoteIpAddress?.ToString(),
                };
                data[dateKey][timeKey][index.ToString()] = logEntry;

                // Serialize the data back to JSON and overwrite the file
                string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
            }

        }
        static bool IsDatabaseAvailable(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection check failed: {ex.Message}");
                return false;
            }
        }
    }
}
