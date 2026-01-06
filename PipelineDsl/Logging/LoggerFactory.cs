using PipelineDsl.Core;

namespace PipelineDsl.Logging;

/// <summary>
/// Factory for creating platform-specific loggers.
/// </summary>
public static class LoggerFactory
{
    public static ILogger Create(CiPlatform platform)
    {
        return platform switch
        {
            CiPlatform.AzureDevOps => new AzureDevOpsLogger(),
            CiPlatform.GitHubActions => new GitHubActionsLogger(),
            _ => new ConsoleLogger()
        };
    }
}