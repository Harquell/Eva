using EVA.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EVA.Common
{
    public class Logger : IInitializable, IDisposable
    {
        public LoggerType Type { get; set; }

        public Logger(LoggerType type)
        {
            Type = type;
        }

        public void Init()
        {
        }

        public enum LoggerType
        {
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL
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