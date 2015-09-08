using CodeFirstConfig;
using System.IO;

namespace Infrastructure
{
    public enum LogType { None, ConfigFromLogFile, RollingFile, WinEventLog, RollingFileAndWinEventLog, VisualStudioTraceOutput }
    public class LogConfigManager : ConfigManager<LogConfig> { }

    public class LogConfig
    {
        private string _logFileName;

        public string FileName
        {
            get { return _logFileName; }
            set
            {
                if (value.Contains("~"))
                    value = value.Replace("~", App.Config.Folder);
                _logFileName = value;
            }
        }

        public string Pattern { get; set; }
        public LogType Type { get; set; }
        public bool IncludeEventId = true;
        public bool IncludeHttpRequest { get; set; }
        public string RollingFileMaximumFileSize { get; set; }
        public string RollingFileDatePattern { get; set; }
        public bool IncludeCallerInfo { get; set; }

        [ConfigComment("0 = No backup files. -1 = Infinite number of backup files. (log4net MaxSizeRollBackups)")]
        public int MaxNumberOfBackupFiles { get; set; }

        public LogConfig BuildPattern()
        {
            Pattern = string.Concat(
                "%property{id}. %-5level [%thread:%property{culture}:%property{uiculture}:%u] %date [%property{instance}",
                string.Concat(App.IsDebugConfiguration ? " DEBUG" : ""),
                string.Concat(App.Debugging ? " DEBUGER" : ""),
                string.Concat(App.Testing ? " TESTING" : ""),
                "] %logger%property{memberName} %newline");
            if (IncludeCallerInfo)
                Pattern = string.Concat(Pattern,
                    "From: %property{source}%newline");
            if (IncludeHttpRequest && App.IsWebApp)
                Pattern = string.Concat(Pattern, "Request: %property{request}%newline");
            Pattern = string.Concat(Pattern, "Message: %message%newline%exception%newline%newline");
            if (!Pattern.Contains("property{id}")) return this;
            Pattern = Pattern.Replace("%property{id}. ", "");
            Pattern = Pattern.Replace("%property{id}.", "");
            Pattern = Pattern.Replace("%property{id}", "");
            return this;
        }

        public LogConfig()
        {
            _logFileName = Path.Combine(App.Config.DataFolder, "LOG.TXT");

            if (App.Debugging || App.Testing)
            {
                Type = LogType.VisualStudioTraceOutput;
            }
            else
            {
                Type = LogType.RollingFile;
            }

            IncludeHttpRequest = App.IsWebApp;
            IncludeCallerInfo = true;

            RollingFileMaximumFileSize = "1MB";
            RollingFileDatePattern = ".yyyy-MM-dd";
            MaxNumberOfBackupFiles = 10;

            BuildPattern();
        }
    }
}