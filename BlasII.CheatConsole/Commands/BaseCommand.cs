using BlasII.ModdingAPI;

namespace BlasII.CheatConsole.Commands;

public abstract class BaseCommand
{
    private readonly string _name;
    public string Name => _name;

    public BaseCommand(string name) => _name = name;

    public abstract void Execute(string[] args);

    public virtual void Update() { }

    protected bool ValidateParameterCount(string[] paramaters, int num)
    {
        bool isValid = paramaters.Length == num;

        if (!isValid)
            ModLog.Error($"This command requires {num} parameters!");

        return isValid;
    }

    protected bool ValidateIntParamater(string parameter, out int result)
    {
        bool isValid = int.TryParse(parameter, out result);

        if (!isValid)
            ModLog.Error($"Parameter '{parameter}' is not a valid integer!");

        return isValid;
    }

    protected void Write(string message) => ModLog.Info("[CONSOLE] " + message);

    protected void WriteFailure(string message) => ModLog.Error("[CONSOLE] " + message);
}
