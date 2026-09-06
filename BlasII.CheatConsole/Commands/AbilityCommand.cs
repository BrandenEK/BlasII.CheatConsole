using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands;

internal class AbilityCommand : ModComplexCommand
{
    public AbilityCommand() : base("ability") { }

    [SubCommand]
    private void List()
    {
        foreach (var ability in AssetStorage.Abilities)
        {
            Write($"{ability.Id}: {ability.StaticId}");
        }
    }

    [SubCommand]
    private void Unlock(string id)
    {
        // Unlock all abilities
        if (id == "all")
        {
            Write("Unlocking all abilities!");
            foreach (var ab in AssetStorage.Abilities)
                CoreCache.AbilitiesUnlockManager.SetAbility(ab.Value, true);
            return;
        }

        // Check if the single ability exists
        if (!AssetStorage.Abilities.TryGetValue(id.ToUpper(), out var ability))
        {
            WriteFailure($"Ability {id} does not exist!");
            return;
        }

        // Unlock the single ability
        Write("Adding ability: " + id);
        CoreCache.AbilitiesUnlockManager.SetAbility(ability, true);
    }

    [SubCommand]
    private void Lock(string id)
    {
        // Lock all abilities
        if (id == "all")
        {
            Write("Locking all abilities!");
            foreach (var ab in AssetStorage.Abilities)
                CoreCache.AbilitiesUnlockManager.SetAbility(ab.Value, false);
            return;
        }

        // Check if the single ability exists
        if (!AssetStorage.Abilities.TryGetValue(id.ToUpper(), out var ability))
        {
            WriteFailure($"Ability {id} does not exist!");
            return;
        }

        // Lock the single ability
        Write("Locking ability: " + id);
        CoreCache.AbilitiesUnlockManager.SetAbility(ability, false);
    }
}
