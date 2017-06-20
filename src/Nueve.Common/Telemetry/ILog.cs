using System;

namespace Nueve.Common.Telemetry
{
    public interface ILog
    {
        void Info(string message);
        void Error(Exception exception);
        void Dependency(string name, string command, DateTimeOffset start, TimeSpan duration, bool success);
        void Metric(string name, double value);
    }
}
