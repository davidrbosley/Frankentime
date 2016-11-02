namespace Frankentime.Domain.Logging
{
    public interface ILogger
    {
        void Log(LoggingCategory category, LoggingSeverity severity, string message);
    }
}
