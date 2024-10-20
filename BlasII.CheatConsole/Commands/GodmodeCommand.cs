using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Helpers;

namespace BlasII.CheatConsole.Commands;

internal class GodmodeCommand : ModCommand
{
    public GodmodeCommand() : base("godmode") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
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
                WriteFailure("Acceptable input is 'on' or 'off'");
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
