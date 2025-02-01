using BlasII.CheatConsole.Conditionals;
using Il2CppTGK.Game;
using Il2CppTGK.Game.PlayerSpawn;
using System.Collections.Generic;

namespace BlasII.CheatConsole.Commands;

internal class LoadCommand : ConditionalCommand
{
    public LoadCommand() : base("load") { }

    protected override IEnumerable<ConditionalTarget> InitializeTargets()
    {
        return
        [
            new ConditionalTarget(args => args.Length == 1, args => LoadRoom(args[0].ToUpper(), 0)),
            new ConditionalTarget(args => args.Length == 2, args => LoadRoom(args[0].ToUpper(), ToInteger(args[1]))),
        ];
    }

    private void LoadRoom(string room, int entry)
    {
        if (!CoreCache.Room.ExistsRoom(room))
        {
            WriteFailure($"Room {room} does not exist");
            return;
        }

        var location = new SceneEntryID()
        {
            scene = room.ToUpper(),
            entryId = entry,
        };

        Write("Teleporting to " + location.scene);
        CoreCache.PlayerSpawn.TeleportPlayer(location, false, null);
    }
}
