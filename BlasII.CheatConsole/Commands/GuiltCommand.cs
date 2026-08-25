using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands;

internal class GuiltCommand : ModCommandFull
{
    public GuiltCommand() : base("guilt") { }

    [SubCommand]
    private void Reset()
    {
        AssetStorage.PlayerStats.SetCurrentValue(AssetStorage.RangeStats["Guilt"], 0);
    }
}
