using BlasII.CheatConsole.Extensions;
using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Items;

internal abstract class BaseItemCommand<T>(string name, TypedStorage<T> storage) : ModCommand(name) where T : ItemID
{
    private readonly TypedStorage<T> _storage = storage;

    public override sealed void Execute(string[] args)
    {
        switch (args[0])
        {
            case "add":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    AddItem(args[1]);
                    break;
                }
            case "remove":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    RemoveItem(args[1]);
                    break;
                }
            case "list":
                {
                    if (!ValidateParameterCount(args, 1))
                        return;

                    ListItems();
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void AddItem(string id)
    {
        // Add all items
        if (id == "all")
        {
            Write($"Adding all {Name}s!");
            foreach (var asset in _storage.AllAssets)
                AssetStorage.PlayerInventory.AddItemAsync(asset.Value);
            return;
        }

        // Check if the single item exists
        if (!_storage.TryGetAsset(id.ToUpper(), out var item))
        {
            WriteFailure($"{Name.Capitalize()} {id} does not exist!");
            return;
        }

        // Add the single item
        Write($"Adding {Name}: " + id);
        AssetStorage.PlayerInventory.AddItemAsync(item);
    }

    private void RemoveItem(string id)
    {
        // Remove all items
        if (id == "all")
        {
            Write($"Removing all {Name}s!");
            foreach (var asset in _storage.AllAssets)
                AssetStorage.PlayerInventory.RemoveItem(asset.Value);
            return;
        }

        // Check if the single item exists
        if (!_storage.TryGetAsset(id.ToUpper(), out var item))
        {
            WriteFailure($"{Name.Capitalize()} {id} does not exist!");
            return;
        }

        // Remove the single item
        Write($"Removing {Name}: " + id);
        AssetStorage.PlayerInventory.RemoveItem(item);
    }

    private void ListItems()
    {
        foreach (var item in _storage.AllAssets)
        {
            Write($"{item.Key}: {item.Value.caption}");
        }
    }
}
