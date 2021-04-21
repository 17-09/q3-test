using System;

namespace ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception exception);
    }
}