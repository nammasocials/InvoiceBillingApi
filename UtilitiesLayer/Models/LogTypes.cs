using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLayer.Models
{
    public class LogTypes
    {
        public enum LogType
        {
            Information,
            CriticalError,
            Error,
            Warning,
        }
    }
    public class LogEntry
    {
        public string LogType { get; set; }
        public string Exception { get; set; }
        public string EndPoint { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string DebugMessage { get; set; }
        public string Ip { get; set; }
    }
}
