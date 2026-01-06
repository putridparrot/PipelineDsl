namespace PipelineDsl.Core;

public class StepResult
{
    public string StepName { get; set; } = string.Empty;
    public PipelineResult? Result { get; set; }
    public TimeSpan Duration { get; set; }
    public bool Skipped { get; set; }
    public Exception? Exception { get; set; }
    public bool Success => Exception == null && (Skipped || Result?.Success == true);
}