using BlasII.ModdingAPI.Storage;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands
{
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
            // Add all weapons
            if (id == "all")
            {
                Write("Adding all weapons!");
                foreach (var w in WeaponStorage.GetAllWeapons())
                    CoreCache.EquipmentManager.Unlock(w.Value);
                return;
            }

            // Check if the single weapon exists
            if (!WeaponStorage.TryGetWeapon(id.ToUpper(), out var weapon))
            {
                WriteFailure($"Weapon {id} does not exist!");
                return;
            }

            // Add the single weapon
            Write("Adding weapon: " + id);
            CoreCache.EquipmentManager.Unlock(weapon);
        }

        private void LockWeapon(string id)
        {
            // Remove all weapons
            if (id == "all")
            {
                Write("Removing all weapons!");
                foreach (var w in WeaponStorage.GetAllWeapons())
                    CoreCache.EquipmentManager.Lock(w.Value);
                return;
            }

            // Check if the single weapon exists
            if (!WeaponStorage.TryGetWeapon(id.ToUpper(), out var weapon))
            {
                WriteFailure($"Weapon {id} does not exist!");
                return;
            }

            // Remove the single weapon
            Write("Removing weapon: " + id);
            CoreCache.EquipmentManager.Lock(weapon);
        }

        private void ListWeapons()
        {
            foreach (var weapon in WeaponStorage.GetAllWeapons())
            {
                Write($"{weapon.Key}: {weapon.Value.name}");
            }
        }
    }
}
