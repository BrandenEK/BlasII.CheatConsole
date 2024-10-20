using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Items;

internal class PrayerCommand : BaseItemCommand<PrayerItemID>
{
    public PrayerCommand() : base("prayer", AssetStorage.Prayers) { }
}
