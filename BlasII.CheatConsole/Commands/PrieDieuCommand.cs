using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands;

internal class PrieDieuCommand : BaseCommand
{
    public PrieDieuCommand() : base("priedieu") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "upgrade":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    UpgradePrieDieus();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void UpgradePrieDieus()
    {
        Write("Fully upgrading prie dieus");
        foreach (var upgrade in CoreCache.PrieDieuManager.config.upgrades)
        {
            CoreCache.PrieDieuManager.Upgrade(upgrade);
        }
    }
}
