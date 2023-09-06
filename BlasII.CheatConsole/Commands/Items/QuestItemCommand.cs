using BlasII.ModdingAPI.Storage;

namespace BlasII.CheatConsole.Commands
{
    internal class QuestItemCommand : BaseCommand
    {
        public QuestItemCommand() : base("questitem") { }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "add":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        AddQuestItem(args[1]);
                        break;
                    }
                case "remove":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        RemoveQuestItem(args[1]);
                        break;
                    }
                case "list":
                    {
                        if (!ValidateParameterCount(args, 1))
                            return;

                        ListQuestItems();
                        break;
                    }
                default:
                    {
                        WriteFailure("Unknown subcommand: " + args[0]);
                        break;
                    }
            }
        }

        private void AddQuestItem(string id)
        {
            // Add all quest items
            if (id == "all")
            {
                Write("Adding all quest items!");
                foreach (var item in ItemStorage.GetAllQuestItems())
                    ItemStorage.PlayerInventory.AddItemAsync(item.Value);
                return;
            }

            // Check if the single quest item exists
            if (!ItemStorage.TryGetQuestItem(id.ToUpper(), out var questitem))
            {
                WriteFailure($"Quest item {id} does not exist!");
                return;
            }

            // Add the single quest item
            Write("Adding quest item: " + id);
            ItemStorage.PlayerInventory.AddItemAsync(questitem);
        }

        private void RemoveQuestItem(string id)
        {
            // Remove all quest items
            if (id == "all")
            {
                Write("Removing all quest items!");
                foreach (var item in ItemStorage.GetAllQuestItems())
                    ItemStorage.PlayerInventory.RemoveItem(item.Value);
                return;
            }

            // Check if the single quest item exists
            if (!ItemStorage.TryGetQuestItem(id.ToUpper(), out var questitem))
            {
                WriteFailure($"Quest item {id} does not exist!");
                return;
            }

            // Remove the single quest item
            Write("Removing quest item: " + id);
            ItemStorage.PlayerInventory.RemoveItem(questitem);
        }

        private void ListQuestItems()
        {
            foreach (var questitem in ItemStorage.GetAllQuestItems())
            {
                Write($"{questitem.Key}: {questitem.Value.caption}");
            }
        }
    }
}
