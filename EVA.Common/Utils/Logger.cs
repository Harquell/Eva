using EVA.Common.Interfaces;
using System;

namespace EVA.Common.Utils
{
    public class Logger : IInitializable, IDisposable
    {
        public LoggerType LoggerLevel { get; set; }
        private static Logger _instance;
        public static Logger Instance => _instance ?? (_instance = new Logger(LoggerType.FATAL));

        public Logger(LoggerType level)
        {
            LoggerLevel = level;
        }

        public void Init()
        {
            //TODO: Load LoggerLevel from configuration
        }

        public enum LoggerType
        {
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL
        }

        public static void Fatal(Exception e)
            => Instance.Log(e.Message, LoggerType.FATAL);

        public static void Fatal(string message)
            => Instance.Log(message, LoggerType.FATAL);

        public static void Error(Exception e)
            => Instance.Log(e.Message, LoggerType.ERROR);

        public static void Error(string message)
            => Instance.Log(message, LoggerType.ERROR);

        public static void Warn(string message)
            => Instance.Log(message, LoggerType.WARN);

        public static void Info(string message)
            => Instance.Log(message, LoggerType.INFO);

        public static void Debug(string message)
            => Instance.Log(message, LoggerType.DEBUG);

        private void Log(string message, LoggerType type)
        {
            if ((int)type >= (int)LoggerLevel)
            {
                Console.WriteLine(string.Format("[{0,-5}] {1}", type.ToString(), message));
                //TODO: Log message
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}