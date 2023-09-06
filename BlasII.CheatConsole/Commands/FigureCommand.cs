using BlasII.ModdingAPI.Storage;

namespace BlasII.CheatConsole.Commands
{
    internal class FigureCommand : BaseCommand
    {
        public FigureCommand() : base("figure") { }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "add":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        AddFigure(args[1]);
                        break;
                    }
                case "remove":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        RemoveFigure(args[1]);
                        break;
                    }
                default:
                    {
                        WriteFailure("Unknown subcommand: " + args[0]);
                        break;
                    }
            }
        }

        private void AddFigure(string id)
        {
            // Add all figures
            if (id == "all")
            {
                Write("Adding all figures!");
                foreach (var item in ItemStorage.GetAllFigures())
                    ItemStorage.PlayerInventory.AddItemAsync(item.Value);
                return;
            }

            // Check if the single figure exists
            if (!ItemStorage.TryGetFigure(id.ToUpper(), out var figure))
            {
                WriteFailure($"Figure {id} does not exist!");
                return;
            }

            // Add the single figure
            Write("Adding figure: " + id);
            ItemStorage.PlayerInventory.AddItemAsync(figure);
        }

        private void RemoveFigure(string id)
        {
            // Remove all figures
            if (id == "all")
            {
                Write("Removing all figures!");
                foreach (var item in ItemStorage.GetAllFigures())
                    ItemStorage.PlayerInventory.RemoveItem(item.Value);
                return;
            }

            // Check if the single figure exists
            if (!ItemStorage.TryGetFigure(id.ToUpper(), out var figure))
            {
                WriteFailure($"Figure {id} does not exist!");
                return;
            }

            // Remove the single figure
            Write("Removing figure: " + id);
            ItemStorage.PlayerInventory.RemoveItem(figure);
        }
    }
}
