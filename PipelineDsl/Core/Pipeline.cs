using PipelineDsl.Logging;
using System.Diagnostics;
using System.Text;

namespace PipelineDsl.Core;

/// <summary>
/// Core pipeline execution utility. Provides static methods for command execution.
/// </summary>
public static class Pipeline
{
    /// <summary>
    /// Execute a command with full process management and CI/CD integration.
    /// </summary>
    public static async Task<PipelineResult> ExecuteAsync(string command, string args, ILogger logger)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = startInfo;

        var output = new StringBuilder();
        var error = new StringBuilder();

        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                output.AppendLine(e.Data);
                logger.LogDebug(e.Data);
            }
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                error.AppendLine(e.Data);
                logger.LogDebug($"[stderr] {e.Data}");
            }
        };

        logger.LogDebug($"Executing: {command} {args}");

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync();

        var result = new PipelineResult
        {
            ExitCode = process.ExitCode,
            Output = output.ToString(),
            Error = error.ToString(),
            Success = process.ExitCode == 0
        };

        return result;
    }
}