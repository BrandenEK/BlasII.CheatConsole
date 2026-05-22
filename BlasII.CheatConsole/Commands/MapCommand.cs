using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasII.CheatConsole.Commands;

internal class MapCommand : ModCommand
{
    public MapCommand() : base("map") { }

    public override void Execute(string[] args)
    {
        Write("Doing map");
    }
}
