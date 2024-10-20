using Il2CppTGK.Game.Components.StatsSystem.Data;

namespace BlasII.CheatConsole.Commands.Stats;

internal class TearsCommand : BaseCommand
{
    public TearsCommand() : base("tears") { }

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

                    AddTears(amount);
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void AddTears(int amount)
    {
        if (StatStorage.TryGetValueStat("Tears", out ValueStatID tears))
        {
            Write($"Adding {amount} tears");
            StatStorage.PlayerStats.AddToCurrentValue(tears, amount);
        }
    }
}
