namespace PipelineDsl.Logging;

public class AzureDevOpsLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine(message);
    }

    public void LogSuccess(string message)
    {
        Console.WriteLine($"##[section]{message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"##vso[task.logissue type=warning]{message}");
    }

    public void LogError(string message, Exception? ex = null)
    {
        var errorMessage = ex != null ? $"{message}: {ex.Message}" : message;
        Console.WriteLine($"##vso[task.logissue type=error]{errorMessage}");

        if (ex != null)
        {
            Console.WriteLine($"##[error]{ex.StackTrace}");
        }
    }

    public void LogDebug(string message)
    {
        Console.WriteLine($"##[debug]{message}");
    }

    public void LogSection(string section, string message)
    {
        Console.WriteLine($"##[section]{section}: {message}");
    }
}