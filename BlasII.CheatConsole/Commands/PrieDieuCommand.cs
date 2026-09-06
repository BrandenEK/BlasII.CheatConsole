using BlasII.CheatConsole.Attributes;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands;

internal class PrieDieuCommand : ModComplexCommand
{
    public PrieDieuCommand() : base("priedieu") { }

    [SubCommand]
    private void Upgrade()
    {
        Write("Fully upgrading prie dieus");
        foreach (var upgrade in CoreCache.PrieDieuManager.config.upgrades)
        {
            CoreCache.PrieDieuManager.Upgrade(upgrade);
        }
    }
}
