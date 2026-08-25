using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasII.CheatConsole;

internal class ModCommandFull : ModCommand
{
    public ModCommandFull(string name) : base(name)
    {
        // Use reflection to cache the subcommands
    }

    public override void Execute(string[] args)
    {
        // help or no parameters will list the valid descriptions

        throw new NotImplementedException();
    }
}
