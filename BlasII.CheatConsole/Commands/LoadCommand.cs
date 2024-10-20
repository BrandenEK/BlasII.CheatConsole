using Il2CppTGK.Game;
using Il2CppTGK.Game.PlayerSpawn;

namespace BlasII.CheatConsole.Commands;

internal class LoadCommand : BaseCommand
{
    public LoadCommand() : base("load") { }

    public override void Execute(string[] args)
    {
        int entry;
        if (args.Length == 1)
        {
            entry = 0;
        }
        else if (args.Length == 2)
        {
            if (!ValidateIntParamater(args[1], out entry))
                return;
        }
        else
        {
            ValidateParameterCount(args, 2);
            return;
        }

        LoadRoom(args[0], entry);
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
