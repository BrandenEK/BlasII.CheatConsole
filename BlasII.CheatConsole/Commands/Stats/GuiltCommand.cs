namespace BlasII.CheatConsole.Commands.Stats;

internal class GuiltCommand : BaseCommand
{
    public GuiltCommand() : base("guilt") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "reset":
                {
                    ResetGuilt();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void ResetGuilt()
    {
        if (StatStorage.TryGetRangeStat("Guilt", out var stat))
        {
            StatStorage.PlayerStats.SetCurrentValue(stat, 0);
        }
    }
}
