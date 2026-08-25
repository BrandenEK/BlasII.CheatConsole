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

    [SubCommand]
    private void List()
    {
        Write("list all weapons");
    }

    [SubCommand]
    private void Lock(string id)
    {
        Write("lock wep " + id);
    }

    [SubCommand]
    private void Unlock(string id)
    {
        Write("unlock wep " + id);
    }

    [SubCommand]
    private void Upgrade(string id, int numTimes)
    {
        for (int i = 0; i < numTimes; i++)
        {
            Write("upgrade wep " + id);
        }
    }
}
