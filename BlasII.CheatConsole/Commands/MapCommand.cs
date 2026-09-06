using BlasII.CheatConsole.Attributes;
using Il2CppSystem.Linq;
using Il2CppTGK.Game;
using System.Linq;

namespace BlasII.CheatConsole.Commands;

internal class MapCommand : ModComplexCommand
{
    public MapCommand() : base("map") { }

    [SubCommand]
    private void Reveal()
    {
        Write("Revealing entire map");

        foreach (var cell in CoreCache.Map.GetAllCells().ToArray())
            CoreCache.Map.RevealCellInPositionWholeMap(cell.key.GetVector2());
    }
}
