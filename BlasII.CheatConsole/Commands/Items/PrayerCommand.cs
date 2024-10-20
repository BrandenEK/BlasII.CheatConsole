namespace BlasII.CheatConsole.Commands.Items;

internal class PrayerCommand : BaseCommand
{
    public PrayerCommand() : base("prayer") { }

    public override void Execute(string[] args)
    {
        switch (args[0])
        {
            case "add":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    AddPrayer(args[1]);
                    break;
                }
            case "remove":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    RemovePrayer(args[1]);
                    break;
                }
            case "list":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    ListPrayers();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void AddPrayer(string id)
    {
        // Add all prayers
        if (id == "all")
        {
            Write("Adding all prayers!");
            foreach (var item in ItemStorage.GetAllPrayers())
                ItemStorage.PlayerInventory.AddItemAsync(item.Value);
            return;
        }

        // Check if the single prayer exists
        if (!ItemStorage.TryGetPrayer(id.ToUpper(), out var prayer))
        {
            WriteFailure($"Prayer {id} does not exist!");
            return;
        }

        // Add the single prayer
        Write("Adding prayer: " + id);
        ItemStorage.PlayerInventory.AddItemAsync(prayer);
    }

    private void RemovePrayer(string id)
    {
        // Remove all prayers
        if (id == "all")
        {
            Write("Removing all prayers!");
            foreach (var item in ItemStorage.GetAllPrayers())
                ItemStorage.PlayerInventory.RemoveItem(item.Value);
            return;
        }

        // Check if the single prayer exists
        if (!ItemStorage.TryGetPrayer(id.ToUpper(), out var prayer))
        {
            WriteFailure($"Prayer {id} does not exist!");
            return;
        }

        // Remove the single prayer
        Write("Removing prayer: " + id);
        ItemStorage.PlayerInventory.RemoveItem(prayer);
    }

    private void ListPrayers()
    {
        foreach (var prayer in ItemStorage.GetAllPrayers())
        {
            Write($"{prayer.Key}: {prayer.Value.caption}");
        }
    }
}
