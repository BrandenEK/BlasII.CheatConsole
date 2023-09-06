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
                        if (ValidateParameterCount(args, 2))
                            AddBead(args[1].ToUpper());
                        break;
                    }
                case "remove":
                    {
                        if (ValidateParameterCount(args, 2))
                            RemoveBead(args[1].ToUpper());
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
            if (!ItemStorage.TryGetRosaryBead(id, out var bead))
            {
                WriteFailure($"Bead {id} does not exist!");
                return;
            }

            Write("Adding bead: " + id);
            ItemStorage.PlayerInventory.AddItemAsync(bead);
        }

        private void RemoveBead(string id)
        {
            if (!ItemStorage.TryGetRosaryBead(id, out var bead))
            {
                WriteFailure($"Bead {id} does not exist!");
                return;
            }

            Write("Removing bead: " + id);
            ItemStorage.PlayerInventory.RemoveItem(bead);
        }
    }
}
