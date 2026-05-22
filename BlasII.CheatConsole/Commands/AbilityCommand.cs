using BlasII.CheatConsole.Conditionals;
using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game;
using System.Collections.Generic;

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

    private void LockAbility(string id)
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

    private void ListAbilities()
    {
        foreach (var ability in AssetStorage.Abilities)
        {
            Write($"{ability.Id}: {ability.StaticId}");
        }
    }
}

internal class AbilityCommand2 : ConditionalCommand
{
    public AbilityCommand2() : base("ability") { }

    protected override IEnumerable<ConditionalMethod> InitializeTargets()
    {
        return
        [
            new ConditionalMethod(args => args.Length == 1 && args[0] == "list", args => ListAbilities()),
            new ConditionalMethod(args => args.Length == 2 && args[0] == "unlock", args => UnlockAbility(args[1])),
            new ConditionalMethod(args => args.Length >= 1 && args[0] == "unlock", args => WriteFailure($"The 'ability unlock' command requires 1 parameter!")),

            new ConditionalMethod(args => args.Length > 0, args => WriteFailure("list subcommands - invalid")),
            new ConditionalMethod(args => true, args => WriteFailure("list subcommands - none")),
        ];
    }


    private void UnlockAbility(string id)
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

    private void LockAbility(string id)
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

    private void ListAbilities()
    {
        foreach (var ability in AssetStorage.Abilities)
        {
            Write($"{ability.Id}: {ability.StaticId}");
        }
    }
}
