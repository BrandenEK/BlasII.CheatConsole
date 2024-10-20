﻿using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Inventory;

namespace BlasII.CheatConsole.Commands.Items;

internal class BeadCommand : ItemCommand<RosaryBeadItemID>
{
    public BeadCommand() : base("bead", AssetStorage.Beads) { }
}
