   namespace PipelineDsl.Logging;

public class GitHubActionsLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine(message);
    }

    public void LogSuccess(string message)
    {
        Console.WriteLine($"✅ {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"::warning::{message}");
    }

    public void LogError(string message, Exception? ex = null)
    {
        var errorMessage = ex != null ? $"{message}: {ex.Message}" : message;
        Console.WriteLine($"::error::{errorMessage}");

        if (ex != null)
        {
            Console.WriteLine($"::error::{ex.StackTrace}");
        }
    }

    public void LogDebug(string message)
    {
        Console.WriteLine($"::debug::{message}");
    }

    public void LogSection(string section, string message)
    {
        Console.WriteLine($"::group::{section}: {message}");
    }
}