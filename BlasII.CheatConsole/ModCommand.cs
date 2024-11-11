using BlasII.ModdingAPI;

namespace BlasII.CheatConsole;

/// <summary>
/// A command that can be executed in the cheat console
/// </summary>
public abstract class ModCommand(string name)
{
    /// <summary>
    /// The name used to execute the command
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Whether this command requires at least one parameter
    /// </summary>
    public virtual bool NeedsParameters { get; } = true;

    /// <summary>
    /// Performs any functions when the command is executed
    /// </summary>
    public abstract void Execute(string[] args);

    /// <summary>
    /// Performs any functions every game frame
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Ensures there is a certain amount of parameters
    /// </summary>
    protected bool ValidateParameterCount(string[] paramaters, int num)
    {
        bool isValid = paramaters.Length == num;

        if (!isValid)
            ModLog.Error($"This command requires {num} parameters!");

        return isValid;
    }

    /// <summary>
    /// Ensures the parameter is a valid integer
    /// </summary>
    protected bool ValidateIntParamater(string parameter, out int result)
    {
        bool isValid = int.TryParse(parameter, out result);

        if (!isValid)
            ModLog.Error($"Parameter '{parameter}' is not a valid integer!");

        return isValid;
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
