using PipelineDsl.Core;

namespace PipelineDsl.Steps;

/// <summary>
/// Executes a shell command as a pipeline step.
/// </summary>
public class CommandStep(string name, string command, string args = "") : IPipelineStep
{
    public string Name { get; } = name;

    public async Task<PipelineResult> ExecuteAsync(PipelineContext context)
    {
        return await Pipeline.ExecuteAsync(command, args, context.Logger);
    }
}