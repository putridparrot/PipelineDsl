using PipelineDsl.Logging;

namespace PipelineDsl.Core;

/// <summary>
/// Base class for all pipeline contexts. Provides access to core execution primitives.
/// Extension methods can target this class to add functionality.
/// </summary>
public class PipelineContext(ILogger logger, CiPlatform platform)
{
    public ILogger Logger { get; } = logger;
    public CiPlatform Platform { get; } = platform;
    public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

    public static PipelineContext Create()
    {
        var platform = PlatformDetector.Detect();
        var logger = LoggerFactory.Create(platform);
        return new PipelineContext(logger, platform);
    }
}
