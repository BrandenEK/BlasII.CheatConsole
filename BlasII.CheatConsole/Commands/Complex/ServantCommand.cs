using BlasII.CheatConsole.Attributes;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers.Data;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole.Commands.Complex;

internal class ServantCommand : ModComplexCommand
{
    private readonly IEnumerable<PlayerFamiliarID> _servants;

    public ServantCommand() : base("servant")
    {
        _servants = Resources.FindObjectsOfTypeAll<PlayerFamiliarID>();
    }

    [SubCommand]
    private void List()
    {
        Write("Available servants:");

        foreach (var servant in _servants)
        {
            string locked = CoreCache.PlayerFamiliarsManager.IsFamiliarUnlocked(servant.id) ? string.Empty : "(Locked)";
            Write($"{servant.name}: Level {CoreCache.PlayerFamiliarsManager.GetFamiliarLevel(servant.id) + 1} {locked}");
        }
    }

    [SubCommand]
    private void Unlock(string id)
    {
        // Unlocks the servant id
    }

    [SubCommand]
    private void Lock(string id)
    {
        // Locks the servant id
    }

    [SubCommand]
    private void Activate(string id)
    {
        // Activates the servant id
    }

    [SubCommand]
    private void GetXP()
    {
        // Prints the current xp
    }

    [SubCommand]
    private void AddXP(int xp)
    {
        // Adds to xp of current servant
    }
}
