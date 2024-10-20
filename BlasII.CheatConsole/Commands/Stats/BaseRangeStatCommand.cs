using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands.Stats;

internal abstract class BaseRangeStatCommand(string name, string statName) : ModCommand(name)
{
    private readonly string _statName = statName;

    public override sealed void Execute(string[] args)
    {
        switch (args[0])
        {
            case "current":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    Current();
                    break;
                }
            case "set":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    if (!ValidateIntParamater(args[1], out int amount))
                        return;

                    Set(amount);
                    break;
                }
            case "fill":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    Fill();
                    break;
                }
            case "upgrade":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    Upgrade();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void Current()
    {
        int amount = AssetStorage.PlayerStats.GetCurrentValue(AssetStorage.RangeStats[_statName]);
        Write($"Current {Name} is {amount}");
    }

    private void Set(int amount)
    {
        Write($"Setting {Name} to {amount}");
        AssetStorage.PlayerStats.SetCurrentValue(AssetStorage.RangeStats[_statName], amount);
    }

    private void Fill()
    {
        Write("Filling " + Name);
        AssetStorage.PlayerStats.SetCurrentToMax(AssetStorage.RangeStats[_statName]);
    }

    private void Upgrade()
    {
        Write("Upgrading " + Name);
        AssetStorage.PlayerStats.Upgrade(AssetStorage.RangeStats[_statName]);
        AssetStorage.PlayerStats.SetCurrentToMax(AssetStorage.RangeStats[_statName]);
    }
}

internal class HealthCommand : BaseRangeStatCommand
{
    public HealthCommand() : base("health", "Health") { }
}

internal class FervourCommand : BaseRangeStatCommand
{
    public FervourCommand() : base("fervour", "Fervour") { }
}

internal class FlaskCommand : BaseRangeStatCommand
{
    public FlaskCommand() : base("flask", "Flask") { }
}
