namespace PipelineDsl.Logging;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"ℹ️  {message}");
    }

    public void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✅ {message}");
        Console.ResetColor();
    }

    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"⚠️  {message}");
        Console.ResetColor();
    }

    public void LogError(string message, Exception? ex = null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        var errorMessage = ex != null ? $"{message}: {ex.Message}" : message;
        Console.WriteLine($"❌ {errorMessage}");
        
        if (ex != null)
        {
            Console.WriteLine($"   {ex.StackTrace}");
        }
        Console.ResetColor();
    }

    public void LogDebug(string message)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"🔍 {message}");
        Console.ResetColor();
    }

    public void LogSection(string section, string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n=== {section}: {message} ===\n");
        Console.ResetColor();
    }
}