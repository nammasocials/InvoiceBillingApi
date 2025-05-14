using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLayer.CommonUtility
{
    public static class RdlcUtility
    {
        public static string fetchReportFromAssemblyUtility(string reportName)
        {
            var assembly = Assembly.Load("FusionAMTAPI");
            var resource = $"FusionAMTAPI.Reports.{reportName}"; // Adjust accordingly

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string reportDefinition = reader.ReadToEnd();
                        return reportDefinition;
                    }
                }
                else
                {
                    throw new Exception($"Report {resource} not found in embedded resources.");
                }
            }
        }
        public static byte[] GenerateRdlcPdfFileBytes(string reportDefinition, string downloadType,
            Dictionary<string, System.Data.DataTable> DataTables,
            Dictionary<string, string> reportParams)
        {
            //var _reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", rdlcFilename);
            //var fileStream = new FileStream(_reportPath, FileMode.Open, FileAccess.Read);
            //var memoryStream = new MemoryStream();

            //Copy file stream to memory stream
            //fileStream.CopyTo(memoryStream);
            //memoryStream.Position = 0;

            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;
            report.EnableHyperlinks = true;
            report.LoadReportDefinition(new StringReader(reportDefinition));
            foreach (var table in DataTables)
            {
                report.DataSources.Add(new ReportDataSource(table.Key, table.Value));

            }
            List<ReportParameter> reportparameter = new List<ReportParameter>();
            foreach (var param in reportParams)
            {
                report.SetParameters(new[] { new ReportParameter(param.Key, param.Value) });
            }
            byte[] pdf = report.Render(downloadType.ToUpper());
            return pdf;
        }
    }
    public static class DatatableConversion
    {
        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all the properties of the class type T
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create a DataColumn for each property
            foreach (PropertyInfo prop in properties)
            {
                Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, type);
            }

            // Populate the DataTable with data from the items
            foreach (T item in items)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
