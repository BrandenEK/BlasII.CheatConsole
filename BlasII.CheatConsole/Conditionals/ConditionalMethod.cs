using System;

namespace BlasII.CheatConsole.Conditionals;

/// <summary>
/// Executes a method upon the condition
/// </summary>
public class ConditionalMethod(Func<string[], bool> condition, Action<string[]> execution) : IConditional
{
    /// <inheritdoc/>
    public Func<string[], bool> Condition { get; } = condition;

    /// <inheritdoc/>
    public Action<string[]> Execution { get; } = execution;
}
