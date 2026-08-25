using BlasII.CheatConsole.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasII.CheatConsole.FullCommands;

internal class NewWeaponCommand : ModCommandFull
{
    public NewWeaponCommand() : base("wep") { }

    [SubCommand("list", 0)]
    private void List()
    {
        Write("list all weapons");
    }

    [SubCommand("lock", 1)]
    private void Lock(string id)
    {
        Write("lock wep id");
    }

    private void Unlock(string id)
    {
        Write("unlock wep id");
    }

    private void Upgrade(string id)
    {
        Write("upgrade wep id");
    }
}
