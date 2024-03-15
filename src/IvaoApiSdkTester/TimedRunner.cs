using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace Ivao.It.IvaoApiSdkTester;

internal interface ITimedRunner
{
    TimedRunner SetLabel(string label);
    void Run(Action action);
    Task<T> Run<T>(Func<Task<T>> function);
}

internal class TimedRunner(ILogger<TimedRunner> logger) : ITimedRunner
{
    private readonly Stopwatch _stopwatch = new();

    public string? Label { get; private set; }
    public LogLevel? Level { get; private set; }

    public TimedRunner SetLabel(string label)
    {
        this.Label = label;
        return this;
    }

    public TimedRunner SetLevel(LogLevel level)
    {
        this.Level = level;
        return this;
    }

    public void Run(Action action)
    {
        logger.Log(Level ?? LogLevel.Information, "Starting timing action {label}", Label);
        try
        {
            _stopwatch.Start();
            action.Invoke();
        }
        finally
        {
            _stopwatch.Stop();
            logger.Log(Level ?? LogLevel.Information, "Action {label} completed in {milliseconds} ms", Label, _stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task<T> Run<T>(Func<Task<T>> function)
    {
        logger.Log(Level ?? LogLevel.Information, "Starting timing action {label}", Label);
        try
        {
            _stopwatch.Start();
            return await function.Invoke();
        }
        finally
        {
            _stopwatch.Stop();
            logger.Log(Level ?? LogLevel.Information, "Action {label} completed in {milliseconds} ms", Label, _stopwatch.ElapsedMilliseconds);
        }
    }
}
