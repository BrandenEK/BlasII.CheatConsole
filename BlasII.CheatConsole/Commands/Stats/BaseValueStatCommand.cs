using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands.Stats;

internal abstract class BaseValueStatCommand(string name, string statName) : BaseCommand(name)
{
    private readonly string _name = name;
    private readonly string _statName = statName;

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "add":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    if (!ValidateIntParamater(args[1], out int amount))
                        return;

                    Add(amount);
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void Add(int amount)
    {
        if (AssetStorage.ValueStats.TryGetAsset(_statName, out var stat))
        {
            Write($"Adding {amount} {_name}");
            AssetStorage.PlayerStats.AddToCurrentValue(stat, amount);
        }
    }
}

internal class TearsCommand : BaseValueStatCommand
{
    public TearsCommand() : base("tears", "Tears") { }
}

internal class MarksCommand : BaseValueStatCommand
{
    public MarksCommand() : base("marks", "Orbs") { }
}
