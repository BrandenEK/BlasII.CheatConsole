using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands;

internal class AbilityCommand : ModCommand
{
    public AbilityCommand() : base("ability") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "unlock":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    UnlockAbility(args[1]);
                    break;
                }
            case "lock":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    LockAbility(args[1]);
                    break;
                }
            case "list":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    ListAbilities();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void UnlockAbility(string id)
    {
        // Unlock all abilities
        if (id == "all")
        {
            Write("Unlocking all abilities!");
            foreach (var ab in AssetStorage.Abilities.AllAssets)
                CoreCache.AbilitiesUnlockManager.SetAbility(ab.Value, true);
            return;
        }

        // Check if the single ability exists
        if (!AssetStorage.Abilities.TryGetAsset(id.ToUpper(), out var ability))
        {
            WriteFailure($"Ability {id} does not exist!");
            return;
        }

        // Unlock the single ability
        Write("Adding ability: " + id);
        CoreCache.AbilitiesUnlockManager.SetAbility(ability, true);
    }

    private void LockAbility(string id)
    {
        // Lock all abilities
        if (id == "all")
        {
            Write("Locking all abilities!");
            foreach (var ab in AssetStorage.Abilities.AllAssets)
                CoreCache.AbilitiesUnlockManager.SetAbility(ab.Value, false);
            return;
        }

        // Check if the single ability exists
        if (!AssetStorage.Abilities.TryGetAsset(id.ToUpper(), out var ability))
        {
            WriteFailure($"Ability {id} does not exist!");
            return;
        }

        // Lock the single ability
        Write("Locking ability: " + id);
        CoreCache.AbilitiesUnlockManager.SetAbility(ability, false);
    }

    private void ListAbilities()
    {
        foreach (var ability in AssetStorage.Abilities.AllAssets)
        {
            Write($"{ability.Key}: {ability.Value.name}");
        }
    }
}
