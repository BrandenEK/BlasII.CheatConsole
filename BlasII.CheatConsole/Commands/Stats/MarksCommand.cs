using Il2CppTGK.Game.Components.StatsSystem.Data;

namespace BlasII.CheatConsole.Commands.Stats;

internal class MarksCommand : BaseCommand
{
    public MarksCommand() : base("marks") { }

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

                    AddMarks(amount);
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void AddMarks(int amount)
    {
        if (StatStorage.TryGetValueStat("Orbs", out ValueStatID orbs))
        {
            Write($"Adding {amount} marks");
            StatStorage.PlayerStats.AddToCurrentValue(orbs, amount);
        }
    }
}
