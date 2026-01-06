using PipelineDsl.Core;

namespace PipelineDsl.Steps;

/// <summary>
/// Builds a .NET project.
/// </summary>
public class DotNetBuildStep(string projectPath = "", string configuration = "Release") : IPipelineStep
{
    public string Name => "Build .NET Project";

    public async Task<PipelineResult> ExecuteAsync(PipelineContext context)
    {
        var args = $"build {projectPath} --configuration {configuration}";
        return await Pipeline.ExecuteAsync("dotnet", args, context.Logger);
    }
}