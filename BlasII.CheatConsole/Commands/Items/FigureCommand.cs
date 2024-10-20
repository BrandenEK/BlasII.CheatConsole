using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Items;

internal class FigureCommand : BaseItemCommand<FigureItemID>
{
    public FigureCommand() : base("figure", AssetStorage.Figures) { }
}
