using System;

namespace BlasII.CheatConsole.Conditionals;

/// <summary>
/// Displays an error upon the condition
/// </summary>
public class ConditionalError(Func<string[], bool> condition, string error, ModCommand cmd) : IConditional
{
    /// <inheritdoc/>
    public Func<string[], bool> Condition { get; } = condition;

    /// <inheritdoc/>
    public Action<string[]> Execution { get; } = (args) => cmd.WriteFailure(error);
}
