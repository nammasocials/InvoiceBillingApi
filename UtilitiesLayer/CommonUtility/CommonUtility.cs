using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilitiesLayer.CommonUtility
{
    public class RandomTextGenerator
    {
        private static Random random = new Random();

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public static class CommonUtility
    {
        public static TDest CopyProperties<TSrc, TDest>(TSrc source, TDest destination, params string[] excludeProperties)
        {
            var sourceProps = typeof(TSrc).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead);
            var destProps = typeof(TDest).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name);

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.TryGetValue(sourceProp.Name, out PropertyInfo destProp))
                {
                    if (excludeProperties != null && excludeProperties.Contains(sourceProp.Name))
                        continue;

                    if (destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destProp.SetValue(destination, sourceProp.GetValue(source, null), null);
                    }
                }
            }

            return destination;
        }
        public static string SerializeStaticClass(Type staticClassType)
        {
            if (!staticClassType.IsAbstract || !staticClassType.IsSealed)
            {
                throw new ArgumentException("Provided type is not a static class.");
            }

            // Use reflection to get static properties and their values
            var properties = staticClassType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            var values = new Dictionary<string, object>();

            foreach (var property in properties)
            {
                values[property.Name] = property.GetValue(null);
            }

            // Serialize to JSON
            return JsonSerializer.Serialize(values, new JsonSerializerOptions { WriteIndented = true });
        }

        public static void DeserializeStaticClass(Type staticClassType, string json)
        {
            if (!staticClassType.IsAbstract || !staticClassType.IsSealed)
            {
                throw new ArgumentException("Provided type is not a static class.");
            }

            // Deserialize JSON into a dictionary
            var values = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            if (values == null) return;

            // Use reflection to set static properties
            var properties = staticClassType.GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (var property in properties)
            {
                if (values.ContainsKey(property.Name))
                {
                    object value = values[property.Name];

                    if (value == null || value.ToString() == null)
                    {
                        // Skip null or undefined values
                        continue;
                    }

                    try
                    {
                        // Handle type conversion
                        if (property.PropertyType == typeof(DateTime))
                        {
                            // Convert to DateTime
                            value = DateTime.Parse(value.ToString());
                        }
                        else if (property.PropertyType.IsEnum)
                        {
                            // Convert to Enum
                            value = Enum.Parse(property.PropertyType, value.ToString());
                        }
                        else if (property.PropertyType == typeof(bool))
                        {
                            // Convert to Boolean
                            value = bool.Parse(value.ToString());
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            // Convert to Integer
                            value = int.Parse(value.ToString());
                        }
                        else if (property.PropertyType == typeof(double))
                        {
                            // Convert to Double
                            value = double.Parse(value.ToString());
                        }
                        else
                        {
                            // General conversion for other types
                            value = Convert.ChangeType(value, property.PropertyType);
                        }

                        // Assign the value to the static property
                        property.SetValue(null, value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to set property '{property.Name}': {ex.Message}");
                    }
                }
            }
        }
        public static string DateTagsReplacement(string tags)
        {
            if (!string.IsNullOrEmpty(tags))
            {
                tags = Regex.Replace(tags, Regex.Escape("{YEAR}"), DateTime.Now.Year.ToString(), RegexOptions.IgnoreCase);
                tags = Regex.Replace(tags, Regex.Escape("{DATE}"), DateTime.Now.Date.ToString(), RegexOptions.IgnoreCase);
                tags = Regex.Replace(tags, Regex.Escape("{MONTH}"), DateTime.Now.Month.ToString(), RegexOptions.IgnoreCase);
            }
            return tags;
        }
        public static string DateStringParse(string dateString, string inputFormat, string outputFormat)
        {
            DateTime parsedDate = DateTime.ParseExact(dateString, inputFormat, CultureInfo.InvariantCulture);
            return parsedDate.ToString(outputFormat);
        }
    }
}
