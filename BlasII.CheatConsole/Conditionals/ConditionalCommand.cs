using BlasII.ModdingAPI;
using System.Collections.Generic;

namespace BlasII.CheatConsole.Conditionals;

/// <summary>
/// A command with easier argument conditions
/// </summary>
public abstract class ConditionalCommand : ModCommand
{
    private readonly IEnumerable<ConditionalTarget> _targets;

    /// <inheritdoc/>
    protected ConditionalCommand(string name) : base(name)
    {
        _targets = InitializeTargets();
    }

    /// <summary>
    /// Executes the first target with a satisfied condition
    /// </summary>
    public sealed override void Execute(string[] args)
    {
        foreach (var target in _targets)
        {
            if (!target.Condition(args))
                continue;

            target.Execution(args);
            return;
        }

        // Make better
        WriteFailure("Error");
    }

    /// <summary>
    /// Stores all conditional targets in the constructor
    /// </summary>
    protected abstract IEnumerable<ConditionalTarget> InitializeTargets();

    /// <summary>
    /// Converts the parameter to an integer, or throws an error
    /// </summary>
    protected int ToInteger(string arg)
    {
        return int.TryParse(arg, out int result)
            ? result
            : throw new System.Exception($"Parameter '{arg}' is not a valid integer!");
    }
}
