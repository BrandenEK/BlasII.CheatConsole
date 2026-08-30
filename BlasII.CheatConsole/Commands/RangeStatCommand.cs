using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands;

internal class RangeStatCommand(string name, string statName) : ModCommandFull(name)
{
    private readonly string _statName = statName;

    [SubCommand]
    private void Current()
    {
        int amount = AssetStorage.PlayerStats.GetCurrentValue(AssetStorage.RangeStats[_statName]);
        Write($"Current {Name} is {amount}");
    }

    [SubCommand]
    private void Set(int amount)
    {
        Write($"Setting {Name} to {amount}");
        AssetStorage.PlayerStats.SetCurrentValue(AssetStorage.RangeStats[_statName], amount);
    }

    [SubCommand]
    private void Fill()
    {
        Write("Filling " + Name);
        AssetStorage.PlayerStats.SetCurrentToMax(AssetStorage.RangeStats[_statName]);
    }

    [SubCommand]
    private void Upgrade()
    {
        Write("Upgrading " + Name);
        AssetStorage.PlayerStats.Upgrade(AssetStorage.RangeStats[_statName]);
        AssetStorage.PlayerStats.SetCurrentToMax(AssetStorage.RangeStats[_statName]);
    }
}
