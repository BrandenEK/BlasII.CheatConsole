using BlasII.CheatConsole.Attributes;
using BlasII.CheatConsole.Extensions;
using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Complex;

internal class ItemCommand<T>(string name, GenericSingleStorage<T> storage) : ModComplexCommand(name) where T : ItemID
{
    private readonly GenericSingleStorage<T> _storage = storage;

    [SubCommand]
    private void List()
    {
        foreach (var item in _storage)
        {
            Write($"{item.Id}: {item.Value.caption}");
        }
    }

    [SubCommand]
    private void Add(string id)
    {
        // Add all items
        if (id == "all")
        {
            Write($"Adding all {Name}s!");
            foreach (var asset in _storage)
                AssetStorage.PlayerInventory.AddItemAsync(asset.Value);
            return;
        }

        // Check if the single item exists
        if (!_storage.TryGetValue(id.ToUpper(), out var item))
        {
            WriteFailure($"{Name.Capitalize()} {id} does not exist!");
            return;
        }

        // Add the single item
        Write($"Adding {Name}: " + id);
        AssetStorage.PlayerInventory.AddItemAsync(item);
    }

    [SubCommand]
    private void Remove(string id)
    {
        // Remove all items
        if (id == "all")
        {
            Write($"Removing all {Name}s!");
            foreach (var asset in _storage)
                AssetStorage.PlayerInventory.RemoveItem(asset.Value);
            return;
        }

        // Check if the single item exists
        if (!_storage.TryGetValue(id.ToUpper(), out var item))
        {
            WriteFailure($"{Name.Capitalize()} {id} does not exist!");
            return;
        }

        // Remove the single item
        Write($"Removing {Name}: " + id);
        AssetStorage.PlayerInventory.RemoveItem(item);
    }
}
