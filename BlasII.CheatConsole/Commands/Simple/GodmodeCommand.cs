using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Helpers;

namespace BlasII.CheatConsole.Commands.Simple;

internal class GodmodeCommand : ModSimpleCommand
{
    public GodmodeCommand() : base("godmode") { }

    [MainCommand]
    private void Execute(string status)
    {
        switch (status)
        {
            case "on":
                Write("Activating godmode");
                _active = true;
                break;
            case "off":
                Write("Deactivating godmode");
                _active = false;
                break;
            default:
                WriteFailure("Acceptable status is 'on' or 'off'");
                break;
        }
    }

    public override void Update()
    {
        if (_active && SceneHelper.GameSceneLoaded)
        {
            AssetStorage.PlayerStats.SetCurrentToMax(AssetStorage.RangeStats["Health"]);
            AssetStorage.PlayerStats.SetCurrentToMax(AssetStorage.RangeStats["Fervour"]);
        }
    }

    private bool _active;
}
