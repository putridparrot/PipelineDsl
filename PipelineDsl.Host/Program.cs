using PipelineDsl.Core;
using PipelineDsl.Steps;

var context = PipelineContext.Create();

var executor = new PipelineExecutor(context)
    .AddStep(new CommandStep("Restore", "dotnet", "restore"))
    .AddStep(new DotNetBuildStep())
    .AddStep(new CommandStep("Run Tests", "dotnet", "test --no-build"))
    .AddStep(new CommandStep("Publish", "dotnet", "publish -c Release"));

var result = await executor.ExecuteAsync();

Environment.Exit(result.Success ? 0 : 1);