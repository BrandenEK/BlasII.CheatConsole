using BlasII.ModdingAPI;
using System;

namespace BlasII.CheatConsole;

/// <summary>
/// A command that can be executed in the cheat console
/// </summary>
public abstract class ModBaseCommand(string name)
{
    /// <summary>
    /// The name used to execute the command
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Performs any functions when the command is executed
    /// </summary>
    public abstract void Execute(string[] args);

    /// <summary>
    /// Performs any functions every game frame
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Converts a string to a valid parameter type
    /// </summary>
    protected object ParseParameter(string input, Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Boolean => Convert.ToBoolean(input),
            TypeCode.Int32 => Convert.ToInt32(input),
            TypeCode.Single => Convert.ToSingle(input),
            TypeCode.String => input,
            _ => throw new NotSupportedException($"Parameter type '{type.Name}' is not supported"),
        };
    }

    /// <summary>
    /// Logs an info message to the console
    /// </summary>
    protected void Write(string message) => ModLog.Info("[CONSOLE] " + message);

    /// <summary>
    /// Logs an error message to the console
    /// </summary>
    protected void WriteFailure(string message) => ModLog.Error("[CONSOLE] " + message);
}
