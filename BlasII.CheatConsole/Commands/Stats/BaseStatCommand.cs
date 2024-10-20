using Il2CppTGK.Game.Components.StatsSystem.Data;

namespace BlasII.CheatConsole.Commands.Stats;

internal abstract class BaseStatCommand : BaseCommand
{
    public BaseStatCommand(string name) : base(name) { }

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

    private RangeStatID GetStat()
    {
        string stat = char.ToUpper(Name[0]) + Name[1..];
        return StatStorage.TryGetRangeStat(stat, out var value) ? value : null;
    }

    private void Current()
    {
        int amount = StatStorage.PlayerStats.GetCurrentValue(GetStat());
        Write($"Current {Name} is {amount}");
    }

    private void Set(int amount)
    {
        Write($"Setting {Name} to {amount}");
        StatStorage.PlayerStats.SetCurrentValue(GetStat(), amount);
    }

    private void Fill()
    {
        Write("Filling " + Name);
        StatStorage.PlayerStats.SetCurrentToMax(GetStat());
    }

    private void Upgrade()
    {
        Write("Upgrading " + Name);
        StatStorage.PlayerStats.Upgrade(GetStat());
        Fill();
    }
}
