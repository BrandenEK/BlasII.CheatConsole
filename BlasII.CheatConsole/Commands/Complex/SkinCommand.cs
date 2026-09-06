using BlasII.CheatConsole.Attributes;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;
using System.Linq;
using UnityEngine;

namespace BlasII.CheatConsole.Commands.Complex;

internal class SkinCommand : ModComplexCommand
{
    public SkinCommand() : base("skin") { }

    [SubCommand]
    private void List()
    {
        var palettes = Resources.FindObjectsOfTypeAll<PaletteID>().OrderBy(x => x.name);

        Write("Available skins:");
        foreach (var palette in palettes)
        {
            Write(palette.name);
        }
    }

    [SubCommand]
    private void Unlock(string id)
    {
        var palettes = Resources.FindObjectsOfTypeAll<PaletteID>().OrderBy(x => x.name);
        var skin = palettes.FirstOrDefault(x => x.name == id.ToUpper());

        if (skin == null)
        {
            WriteFailure($"The skin {id} does not exist.");
            return;
        }

        Write($"Unlocking skin {skin.name}");
        CoreCache.PlayerRecolorManager.UnlockSkin(skin);
    }

    [SubCommand]
    private void Set(string id)
    {
        var palettes = Resources.FindObjectsOfTypeAll<PaletteID>().OrderBy(x => x.name);
        var skin = palettes.FirstOrDefault(x => x.name == id.ToUpper());

        if (skin == null)
        {
            WriteFailure($"The skin {id} does not exist.");
            return;
        }

        Write($"Setting current skin to {skin.name}");
        CoreCache.PlayerRecolorManager.SetPalette(skin);
    }
}
