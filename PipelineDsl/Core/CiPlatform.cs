namespace PipelineDsl.Core;

/// <summary>
/// Represents the CI/CD platform the pipeline is running on.
/// </summary>
public enum CiPlatform
{
    Local,
    AzureDevOps,
    GitHubActions,
    Unknown
}