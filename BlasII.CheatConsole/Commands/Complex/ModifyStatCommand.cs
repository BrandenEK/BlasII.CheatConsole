using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands.Complex;

internal class ModifyStatCommand(string name, string statName) : ModComplexCommand(name)
{
    private readonly string _statName = statName;

    [SubCommand]
    private void Add(int amount)
    {
        Write($"Adding {amount} {Name}");
        AssetStorage.PlayerStats.AddBonus(AssetStorage.ModifiableStats[_statName], "cheat", amount, 0);
    }
}
