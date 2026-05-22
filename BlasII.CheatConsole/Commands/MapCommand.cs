using Il2CppSystem.Linq;
using Il2CppTGK.Game;
using System.Linq;

namespace BlasII.CheatConsole.Commands;

internal class MapCommand : ModCommand
{
    public MapCommand() : base("map") { }

    public override void Execute(string[] args)
    {
        if (!ValidateParameterCount(args, 1))
            return;

        if (args[0] != "reveal")
        {
            WriteFailure("Unknown subcommand: " + args[0]);
            return;
        }

        Write("Revealing entire map");
        
        foreach (var cell in CoreCache.Map.GetAllCells().ToArray())
            CoreCache.Map.RevealCellInPositionWholeMap(cell.key.GetVector2());
    }
}
