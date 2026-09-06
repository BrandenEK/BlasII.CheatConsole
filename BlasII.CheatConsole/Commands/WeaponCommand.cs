using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands;

internal class WeaponCommand : ModComplexCommand
{
    public WeaponCommand() : base("weapon") { }

    [SubCommand]
    private void List()
    {
        foreach (var weapon in AssetStorage.Weapons)
        {
            Write($"{weapon.Id}: {weapon.StaticId}");
        }
    }

    [SubCommand]
    private void Unlock(string id)
    {
        // Unlock all weapons
        if (id == "all")
        {
            Write("Unlocking all weapons!");
            foreach (var w in AssetStorage.Weapons)
                CoreCache.EquipmentManager.Unlock(w.Value);
            return;
        }

        // Check if the single weapon exists
        if (!AssetStorage.Weapons.TryGetValue(id.ToUpper(), out var weapon))
        {
            WriteFailure($"Weapon {id} does not exist!");
            return;
        }

        // Unlock the single weapon
        Write("Unlocking weapon: " + id);
        CoreCache.EquipmentManager.Unlock(weapon);
    }

    [SubCommand]
    private void Lock(string id)
    {
        // Lock all weapons
        if (id == "all")
        {
            Write("Locking all weapons!");
            foreach (var w in AssetStorage.Weapons)
                CoreCache.EquipmentManager.Lock(w.Value);
            return;
        }

        // Check if the single weapon exists
        if (!AssetStorage.Weapons.TryGetValue(id.ToUpper(), out var weapon))
        {
            WriteFailure($"Weapon {id} does not exist!");
            return;
        }

        // Lock the single weapon
        Write("Locking weapon: " + id);
        CoreCache.EquipmentManager.Lock(weapon);
    }

    [SubCommand]
    private void Upgrade(string id)
    {
        // Check if the weapon exists
        if (!AssetStorage.Weapons.TryGetValue(id.ToUpper(), out var weapon))
        {
            WriteFailure($"Weapon {id} does not exist!");
            return;
        }

        // Upgrade the weapon
        Write("Upgrading weapon: " + id);
        CoreCache.WeaponMemoryManager.UpgradeWeaponTier(weapon);
    }
}
