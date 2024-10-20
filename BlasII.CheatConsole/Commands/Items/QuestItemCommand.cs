using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Items;

internal class QuestItemCommand : ItemCommand<QuestItemID>
{
    public QuestItemCommand() : base("questitem", AssetStorage.QuestItems) { }
}
