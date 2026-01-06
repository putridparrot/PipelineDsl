using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineDsl.Logging;

public interface ILogger
{
    void LogInfo(string message);
    void LogSuccess(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
    void LogDebug(string message);
    void LogSection(string section, string message);
}