namespace PipelineDsl.Core;

/// <summary>
/// Detects which CI/CD platform the pipeline is running on.
/// </summary>
public static class PlatformDetector
{
    public static CiPlatform Detect()
    {
        // Azure DevOps sets TF_BUILD
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD")))
        {
            return CiPlatform.AzureDevOps;
        }

        // GitHub Actions sets GITHUB_ACTIONS
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS")))
        {
            return CiPlatform.GitHubActions;
        }

        return CiPlatform.Local;
    }
}