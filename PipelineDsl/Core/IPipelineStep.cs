namespace PipelineDsl.Core;

/// <summary>
/// Interface for pipeline steps. Implement this to create reusable, testable pipeline steps.
/// </summary>
public interface IPipelineStep
{
    /// <summary>
    /// The name of the step for logging purposes.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Execute the step logic.
    /// </summary>
    Task<PipelineResult> ExecuteAsync(PipelineContext context);

    /// <summary>
    /// Determines if the step should be skipped based on context.
    /// </summary>
    bool ShouldExecute(PipelineContext context) => true;
}