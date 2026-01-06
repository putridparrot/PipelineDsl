namespace PipelineDsl.Core;

public class PipelineExecutionResult
{
    public bool Success { get; set; }
    public List<StepResult> Steps { get; set; } = new();
    public TimeSpan TotalDuration { get; set; }
    public string? FailedStep { get; set; }
}