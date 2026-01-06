using PipelineDsl.Logging;

namespace PipelineDsl.Core;

/// <summary>
/// Base class for all pipeline contexts. Provides access to core execution primitives.
/// Extension methods can target this class to add functionality.
/// </summary>
public class PipelineContext(ILogger logger, CiPlatform platform)
{
    private readonly Dictionary<string, object> _data = new();

    public ILogger Logger { get; } = logger;
    public CiPlatform Platform { get; } = platform;
    public string WorkingDirectory { get; set; } = Environment.CurrentDirectory;
    public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

    public static PipelineContext Create()
    {
        var platform = PlatformDetector.Detect();
        var logger = LoggerFactory.Create(platform);
        return new PipelineContext(logger, platform);
    }

    /// <summary>
    /// Store arbitrary data in the context for sharing between steps.
    /// </summary>
    public void SetValue<T>(string key, T value) where T : notnull
    {
        _data[key] = value;
    }

    /// <summary>
    /// Retrieve data from the context.
    /// </summary>
    public T? GetValue<T>(string key)
    {
        if (_data.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    /// <summary>
    /// Check if a value exists in the context.
    /// </summary>
    public bool HasValue(string key) => _data.ContainsKey(key);
}
