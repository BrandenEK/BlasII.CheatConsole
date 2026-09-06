using BlasII.CheatConsole.Attributes;
using Il2CppTGK.Game;
using Il2CppTGK.Game.PlayerSpawn;

namespace BlasII.CheatConsole.Commands.Simple;

internal class LoadCommand : ModSimpleCommand
{
    public LoadCommand() : base("load") { }

    [MainCommand]
    private void Execute(string room)
    {
        room = room.ToUpper();
        int entry = 0;

        if (!CoreCache.Room.ExistsRoom(room))
        {
            WriteFailure($"Room {room} does not exist");
            return;
        }

        LoadRoom(room, entry);
    }

    private void LoadRoom(string room, int entry)
    {
        var location = new SceneEntryID()
        {
            scene = room.ToUpper(),
            entryId = entry,
        };

        Write("Teleporting to " + location.scene);
        CoreCache.PlayerSpawn.TeleportPlayer(location, false, null);
    }
}
