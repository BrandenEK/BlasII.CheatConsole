using Il2CppTGK.Game;
using Il2CppTGK.Game.PlayerSpawn;

namespace BlasII.CheatConsole.Commands;

internal class LoadCommand : ModCommand
{
    public LoadCommand() : base("load") { }

    public override void Execute(string[] args)
    {
        string scene;
        int entry;

        if (args.Length == 1)
        {
            scene = args[0].ToUpper();
            entry = 0;
        }
        else if (args.Length == 2)
        {
            scene = args[0].ToUpper();
            if (!ValidateIntParamater(args[1], out entry))
                return;
        }
        else
        {
            ValidateParameterCount(args, 2);
            return;
        }

        if (!CoreCache.Room.ExistsRoom(scene))
        {
            WriteFailure($"Room {scene} does not exist");
            return;
        }

        LoadRoom(scene, entry);
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
