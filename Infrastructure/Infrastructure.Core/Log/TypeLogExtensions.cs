using System;

namespace Infrastructure
{
    public static class TypeLogExtensions
    {
        public static string Error(this Type type, object message)
        {
            return Log.Error(message, type.FullName);
        }

        public static string Warning(this Type type, object message)
        {
            return Log.Warning(message, type.FullName);
        }

        public static string Information(this Type type, object message)
        {
            return Log.Info(message, type.FullName);
        }

        public static string ErrorRefMessage(this Type type, object message)
        {
            return Log.ErrorRefMessage(message, type.FullName);
        }       
    }
}
