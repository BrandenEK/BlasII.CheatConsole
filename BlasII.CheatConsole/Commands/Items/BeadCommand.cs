using BlasII.ModdingAPI.Storage;

namespace BlasII.CheatConsole.Commands
{
    internal class BeadCommand : BaseCommand
    {
        public BeadCommand() : base("bead") { }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "add":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        AddBead(args[1]);
                        break;
                    }
                case "remove":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        RemoveBead(args[1]);
                        break;
                    }
                case "list":
                    {
                        if (!ValidateParameterCount(args, 1))
                            return;

                        ListBeads();
                        break;
                    }
                default:
                    {
                        WriteFailure("Unknown subcommand: " + args[0]);
                        break;
                    }
            }
        }

        private void AddBead(string id)
        {
            // Add all beads
            if (id == "all")
            {
                Write("Adding all beads!");
                foreach (var item in ItemStorage.GetAllRosaryBeads())
                    ItemStorage.PlayerInventory.AddItemAsync(item.Value);
                return;
            }

            // Check if the single bead exists
            if (!ItemStorage.TryGetRosaryBead(id.ToUpper(), out var bead))
            {
                WriteFailure($"Bead {id} does not exist!");
                return;
            }

            // Add the single bead
            Write("Adding bead: " + id);
            ItemStorage.PlayerInventory.AddItemAsync(bead);
        }

        private void RemoveBead(string id)
        {
            // Remove all beads
            if (id == "all")
            {
                Write("Removing all beads!");
                foreach (var item in ItemStorage.GetAllRosaryBeads())
                    ItemStorage.PlayerInventory.RemoveItem(item.Value);
                return;
            }

            // Check if the single bead exists
            if (!ItemStorage.TryGetRosaryBead(id.ToUpper(), out var bead))
            {
                WriteFailure($"Bead {id} does not exist!");
                return;
            }

            // Remove the single bead
            Write("Removing bead: " + id);
            ItemStorage.PlayerInventory.RemoveItem(bead);
        }

        private void ListBeads()
        {
            foreach (var bead in ItemStorage.GetAllRosaryBeads())
            {
                Write($"{bead.Key}: {bead.Value.caption}");
            }
        }
    }
}
