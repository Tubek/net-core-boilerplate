using System;
using System.Reflection;
using Nueve.Common.Config;

namespace Nueve.Common.Telemetry
{
    public class Log : ILog
    {
        private readonly string _app;
        private readonly bool _isLoggingEnabled;

        public Log(IAppConfig appConfig)
        {
            _isLoggingEnabled = appConfig.LoggingEnabled;

            var assembly = Assembly.GetEntryAssembly();
            _app = assembly.GetName().Name;
        }

        public void Info(string message)
        {
            if (_isLoggingEnabled)
            {
                // TODO: log info
            }
        }

        public void Metric(string name, double value)
        {
            if (_isLoggingEnabled)
            {
                // TODO: log metric
            }
        }

        public void Dependency(string name, string command, DateTimeOffset start, TimeSpan duration, bool success)
        {
            if (_isLoggingEnabled)
            {
                // TODO: log dependency
            }
        }

        public void Error(Exception exception)
        {
            if (_isLoggingEnabled)
            {
                // TODO: log error
            }
        }
    }
}
