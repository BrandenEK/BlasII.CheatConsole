using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Items;

internal class FigureCommand : ItemCommand<FigureItemID>
{
    public FigureCommand() : base("figure", AssetStorage.Figures) { }
}
