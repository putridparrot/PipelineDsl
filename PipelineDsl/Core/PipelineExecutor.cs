using System.Diagnostics;

namespace PipelineDsl.Core;

/// <summary>
/// Executes a sequence of pipeline steps with error handling and logging.
/// </summary>
public class PipelineExecutor(PipelineContext context)
{
    private readonly List<IPipelineStep> _steps = new();

    /// <summary>
    /// Add a step to the pipeline.
    /// </summary>
    public PipelineExecutor AddStep(IPipelineStep step)
    {
        _steps.Add(step);
        return this;
    }

    /// <summary>
    /// Execute all registered steps in sequence.
    /// </summary>
    public async Task<PipelineExecutionResult> ExecuteAsync()
    {
        var results = new List<StepResult>();
        var stopwatch = Stopwatch.StartNew();

        context.Logger.LogSection("Pipeline", $"Starting pipeline execution on {context.Platform}");

        foreach (var step in _steps)
        {
            if (context.CancellationToken.IsCancellationRequested)
            {
                context.Logger.LogWarning("Pipeline execution cancelled");
                break;
            }

            if (!step.ShouldExecute(context))
            {
                context.Logger.LogInfo($"[{step.Name}] Skipped");
                results.Add(new StepResult
                {
                    StepName = step.Name,
                    Skipped = true
                });
                continue;
            }

            context.Logger.LogSection("Step", step.Name);
            var stepWatch = Stopwatch.StartNew();

            try
            {
                var result = await step.ExecuteAsync(context);
                stepWatch.Stop();

                var stepResult = new StepResult
                {
                    StepName = step.Name,
                    Result = result,
                    Duration = stepWatch.Elapsed,
                    Skipped = false
                };

                results.Add(stepResult);

                if (result.Success)
                {
                    context.Logger.LogSuccess($"[{step.Name}] Completed in {stepWatch.Elapsed.TotalSeconds:F2}s");
                }
                else
                {
                    context.Logger.LogError($"[{step.Name}] Failed with exit code {result.ExitCode}");
                    
                    // Emit platform-specific failure markers
                    EmitFailureMarker(context.Platform, step.Name);
                    
                    return new PipelineExecutionResult
                    {
                        Success = false,
                        Steps = results,
                        TotalDuration = stopwatch.Elapsed,
                        FailedStep = step.Name
                    };
                }
            }
            catch (Exception ex)
            {
                stepWatch.Stop();
                context.Logger.LogError($"[{step.Name}] Exception occurred", ex);

                results.Add(new StepResult
                {
                    StepName = step.Name,
                    Exception = ex,
                    Duration = stepWatch.Elapsed
                });

                EmitFailureMarker(context.Platform, step.Name);

                return new PipelineExecutionResult
                {
                    Success = false,
                    Steps = results,
                    TotalDuration = stopwatch.Elapsed,
                    FailedStep = step.Name
                };
            }
        }

        stopwatch.Stop();
        context.Logger.LogSuccess($"Pipeline completed successfully in {stopwatch.Elapsed.TotalSeconds:F2}s");

        return new PipelineExecutionResult
        {
            Success = true,
            Steps = results,
            TotalDuration = stopwatch.Elapsed
        };
    }

    private static void EmitFailureMarker(CiPlatform platform, string stepName)
    {
        switch (platform)
        {
            case CiPlatform.AzureDevOps:
                Console.WriteLine($"##vso[task.logissue type=error]{stepName} failed");
                Console.WriteLine("##vso[task.complete result=Failed;]");
                break;
            case CiPlatform.GitHubActions:
                Console.WriteLine($"::error::{stepName} failed");
                break;
        }
    }
}