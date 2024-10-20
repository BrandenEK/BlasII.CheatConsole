using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands;

internal class WeaponCommand : BaseCommand
{
    public WeaponCommand() : base("weapon") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "unlock":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    UnlockWeapon(args[1]);
                    break;
                }
            case "lock":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    LockWeapon(args[1]);
                    break;
                }
            case "upgrade":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    UpgradeWeapon(args[1]);
                    break;
                }
            case "list":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    ListWeapons();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void UnlockWeapon(string id)
    {
        // Unlock all weapons
        if (id == "all")
        {
            Write("Unlocking all weapons!");
            foreach (var w in AssetStorage.Weapons.AllAssets)
                CoreCache.EquipmentManager.Unlock(w.Value);
            return;
        }

        // Check if the single weapon exists
        if (!AssetStorage.Weapons.TryGetAsset(id.ToUpper(), out var weapon))
        {
            WriteFailure($"Weapon {id} does not exist!");
            return;
        }

        // Unlock the single weapon
        Write("Unlocking weapon: " + id);
        CoreCache.EquipmentManager.Unlock(weapon);
    }

    private void LockWeapon(string id)
    {
        // Lock all weapons
        if (id == "all")
        {
            Write("Locking all weapons!");
            foreach (var w in AssetStorage.Weapons.AllAssets)
                CoreCache.EquipmentManager.Lock(w.Value);
            return;
        }

        // Check if the single weapon exists
        if (!AssetStorage.Weapons.TryGetAsset(id.ToUpper(), out var weapon))
        {
            WriteFailure($"Weapon {id} does not exist!");
            return;
        }

        // Lock the single weapon
        Write("Locking weapon: " + id);
        CoreCache.EquipmentManager.Lock(weapon);
    }

    private void UpgradeWeapon(string id)
    {
        // Check if the weapon exists
        if (!AssetStorage.Weapons.TryGetAsset(id.ToUpper(), out var weapon))
        {
            WriteFailure($"Weapon {id} does not exist!");
            return;
        }

        // Upgrade the weapon
        Write("Upgrading weapon: " + id);
        CoreCache.WeaponMemoryManager.UpgradeWeaponTier(weapon);
    }

    private void ListWeapons()
    {
        foreach (var weapon in AssetStorage.Weapons.AllAssets)
        {
            Write($"{weapon.Key}: {weapon.Value.name}");
        }
    }
}
