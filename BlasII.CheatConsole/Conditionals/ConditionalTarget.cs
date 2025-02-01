using System;

namespace BlasII.CheatConsole.Conditionals;

/// <summary>
/// A method that is executed if the condition is met
/// </summary>
public class ConditionalTarget(Func<string[], bool> condition, Action<string[]> execution)
{
    /// <summary>
    /// The condition that is checked before execution
    /// </summary>
    public Func<string[], bool> Condition { get; } = condition;

    /// <summary>
    /// The method executed if the condition is met
    /// </summary>
    public Action<string[]> Execution { get; } = execution;
}
