using BlasII.CheatConsole.Attributes;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.CheatConsole.Commands.Complex;

internal class ServantCommand : ModComplexCommand
{
    private readonly IEnumerable<PlayerFamiliarID> _servants;

    public ServantCommand() : base("servant")
    {
        _servants = Resources.FindObjectsOfTypeAll<PlayerFamiliarID>().OrderBy(x => x.name);
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
        var servant = _servants.First(x => x.name == id.ToUpper());

        if (servant == null)
        {
            WriteFailure($"The servant {id} does not exist.");
            return;
        }

        Write($"Unlocking servant {servant.name}");
        CoreCache.PlayerFamiliarsManager.UnlockFamiliar(servant.id);
    }

    [SubCommand]
    private void Lock(string id)
    {
        var servant = _servants.First(x => x.name == id.ToUpper());

        if (servant == null)
        {
            WriteFailure($"The servant {id} does not exist.");
            return;
        }

        Write($"Locking servant {servant.name}");
        CoreCache.PlayerFamiliarsManager.LockFamiliar(servant.id);
    }

    [SubCommand]
    private void Activate(string id)
    {
        var servant = _servants.First(x => x.name == id.ToUpper());

        if (servant == null)
        {
            WriteFailure($"The servant {id} does not exist.");
            return;
        }

        Write($"Activating servant {servant.name}");
        CoreCache.PlayerFamiliarsManager.ActivateFamiliar(servant);
    }

    [SubCommand]
    private void Deactivate()
    {
        Write("Deactivating current servant");
        CoreCache.PlayerFamiliarsManager.DeactivateCurrentFamiliar();
    }

    [SubCommand]
    private void GetXP()
    {
        int xp = CoreCache.PlayerFamiliarsManager.GetFamiliarExp(CoreCache.PlayerFamiliarsManager.GetCurrentFamiliarID());
        Write($"Current servant xp: {xp}");
    }

    [SubCommand]
    private void AddXP(int amount)
    {
        Write($"Adding {amount} to current servant");
        CoreCache.PlayerFamiliarsManager.AddExpToCurrentFamiliar(amount);
    }
}
