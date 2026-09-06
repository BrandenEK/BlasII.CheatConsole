using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands.Complex;

internal class ValueStatCommand(string name, string statName) : ModComplexCommand(name)
{
    private readonly string _statName = statName;

    [SubCommand]
    private void Current()
    {
        int amount = AssetStorage.PlayerStats.GetCurrentValue(AssetStorage.ValueStats[_statName]);
        Write($"Current {Name} is {amount}");
    }

    [SubCommand]
    private void Add(int amount)
    {
        Write($"Adding {amount} {Name}");
        AssetStorage.PlayerStats.AddToCurrentValue(AssetStorage.ValueStats[_statName], amount);
    }
}
