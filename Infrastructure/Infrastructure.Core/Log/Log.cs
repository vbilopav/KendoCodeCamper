using CodeFirstConfig;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;

namespace Infrastructure
{
    public static class Log
    {
        private static ulong _id = 0;
        private static readonly object Idlock = new object();
        private static readonly LogConfig Config;

        static Log()
        {
            Config = LogConfigManager.Config;
            PatternLayout layout;
            AppenderSkeleton appender;

            switch (Config.Type)
            {
                case LogType.ConfigFromLogFile:
                    XmlConfigurator.ConfigureAndWatch(new FileInfo(Config.FileName));
                    break;
                case LogType.RollingFile:
                case LogType.RollingFileAndWinEventLog:
                    layout = new PatternLayout(Config.Pattern);
                    appender = new RollingFileAppender
                    {
                        File = Config.FileName,
                        AppendToFile = true,
                        RollingStyle = RollingFileAppender.RollingMode.Composite,
                        MaxSizeRollBackups = Config.MaxNumberOfBackupFiles,// App.Config.IsDebugConfiguration ? -1 : 100,
                        MaximumFileSize = Config.RollingFileMaximumFileSize,
                        DatePattern = Config.RollingFileDatePattern,
                        Layout = layout,
                        LockingModel = new FileAppender.MinimalLock()
                    };
                    layout.ActivateOptions();
                    appender.ActivateOptions();
                    BasicConfigurator.Configure(appender);
                    break;
                default:
                    if (Config.Type == LogType.WinEventLog ||
                        Config.Type == LogType.RollingFileAndWinEventLog)
                    {
                        layout = new PatternLayout(Config.Pattern);               
                        appender = new EventLogAppender
                        {
                            LogName = App.Config.Name,
                            Layout = layout
                        };
                        layout.ActivateOptions();
                        appender.ActivateOptions();
                        BasicConfigurator.Configure(appender);
                    }
                    else if (Config.Type == LogType.VisualStudioTraceOutput)
                    {
                        layout = new PatternLayout(Config.Pattern);
                        appender = new TraceAppender
                        {
                            ImmediateFlush = true                    
                        };
                        layout.ActivateOptions();
                        appender.ActivateOptions();
                        appender.Layout = layout;
                        BasicConfigurator.Configure(appender);
                    }
                    break;
            }
        }

        private enum LogMethod { Error, Warning, Info, Debug }

        private static void Inc()
        {
            if (!Config.IncludeEventId) return;
            lock (Idlock)
            {
                if (_id == ulong.MaxValue) 
                    _id = 0;
                else
                     ++_id;                
            }
            ThreadContext.Properties["id"]  = string.Concat(App.InstanceHash, "-", _id);
        }

        private static string RetreiveSource(string source)
        {
            if (source != null) return source;
            try
            {
                StackFrame frame = new StackTrace().GetFrame(3);
                MethodBase method = frame.GetMethod();
                return method.ReflectedType != null ? method.ReflectedType.FullName : method.Name;
            }
            catch
            {
                return "LOG";
            }            
        }

        private static string FormatHttpRequest()
        {
            try
            {                            
                if (HttpContext.Current == null || HttpContext.Current.Handler == null)
                    return "NOT AVAILABLE";
                return
                    string.Concat(HttpContext.Current.Request.HttpMethod, " ", HttpContext.Current.Request.RawUrl);        
            }
            catch 
            {
                return "NOT AVAILABLE";
            }           
        }

        private static string LogInternal(
            object message, 
            LogMethod method, 
            string source = null, string memberName = null, string sourceFilePath = null, int sourceLineNumber = 0)
        {
            Inc();
            if (Config.IncludeEventId && message is Exception)
            {
                ((Exception) message).Data["_id"] = ThreadContext.Properties["id"];
            };
            ILog log;
            if (source != null) memberName = "";
            if (memberName != "") memberName = string.Concat(".", memberName);
            ThreadContext.Properties["memberName"] = memberName;
            if (sourceLineNumber == 0)
                ThreadContext.Properties["source"] = sourceFilePath;
            else
                ThreadContext.Properties["source"] = string.Concat(sourceFilePath, ":line ", sourceLineNumber);
            ThreadContext.Properties["culture"] = Thread.CurrentThread.CurrentCulture.ToString();
            ThreadContext.Properties["uiculture"] = Thread.CurrentThread.CurrentUICulture.ToString();
            ThreadContext.Properties["request"] = FormatHttpRequest();
            ThreadContext.Properties["instance"] = App.Config.InstanceId;
            var exception = message as Exception;
            if (exception != null)
            {
                Exception e = exception;
                log = LogManager.GetLogger(source ?? e.Source);
                var msg = e.Message;
                if (e.Data["SQL"] != null)
                {
                    msg = string.Concat(msg, "\r\n", "SQL: ", e.Data["SQL"]);
                }
                switch (method)
                {
                    case LogMethod.Error: log.Error(msg, e); break;
                    case LogMethod.Warning: log.Warn(msg, e); break;
                    case LogMethod.Info: log.Info(msg, e); break;
                    case LogMethod.Debug: log.Debug(msg, e); break;
                }
            }
            else
            {
                log = LogManager.GetLogger(RetreiveSource(source));
                switch (method)
                {
                    case LogMethod.Error: log.Error(message); break;
                    case LogMethod.Warning: log.Warn(message); break;
                    case LogMethod.Info: log.Info(message); break;
                    case LogMethod.Debug: log.Debug(message); break;
                }
            }
            return ThreadContext.Properties["id"].ToString();
        }

        public static string Error(object message,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)

        {
            return
                LogInternal(message, LogMethod.Error, source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Error(
            string message, 
            object[] format,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)
        {
            return
                Error(string.Format(message, format), source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Warning(object message,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)

        {
            return
                LogInternal(message, LogMethod.Warning, source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Warning(
            string message,
            object[] format,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)
        {
            return
                Warning(string.Format(message, format), source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Info(object message,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)

        {
            return
                LogInternal(message, LogMethod.Info, source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Info(
            string message,
            object[] format,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)
        {
            return
                Info(string.Format(message, format), source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Debug(object message, 
            string source = null,                    
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)

        {
            return LogInternal(message, LogMethod.Debug, source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string Debug(
            string message,
            object[] format,
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)
        {
            return
                Debug(string.Format(message, format), source, memberName, sourceFilePath, sourceLineNumber);
        }

        public static string ErrorRefMessage(
            object message,                        
            string source = null,
            [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath]string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber]int sourceLineNumber = 0)

        {
            if (Config.IncludeEventId)
            {
                return string.Format(_resx.Core.ErrorMessage,
                    LogInternal(message, LogMethod.Error, source, memberName, sourceFilePath, sourceLineNumber));
            }
            LogInternal(message, LogMethod.Error, source, memberName, sourceFilePath, sourceLineNumber);
            return _resx.Core.GenericError;
        }        
    }
}
