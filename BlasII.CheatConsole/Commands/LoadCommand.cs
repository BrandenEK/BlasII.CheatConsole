using Il2CppTGK.Game;
using Il2CppTGK.Game.PlayerSpawn;
using System;
using System.Collections.Generic;

namespace BlasII.CheatConsole.Commands
{
    internal class LoadCommand : BaseCommand
    {
        public LoadCommand() : base("load") { }

        private void LoadRoom(string[] paramaters)
        {
            string room;
            int entry;

            if (paramaters.Length == 2)
            {
                room = paramaters[0];
                if (!ValidateIntParamater(paramaters[1], out entry))
                    return;
            }
            else if (paramaters.Length == 1)
            {
                room = paramaters[0];
                entry = 0;
            }
            else
            {
                ValidateParameterCount("room", paramaters, 2);
                return;
            }

            var location = new SceneEntryID()
            {
                scene = room.ToUpper(),
                entryId = entry,
            };

            Write("Teleporting to " +  location.scene);
            CoreCache.PlayerSpawn.TeleportPlayer(location, false, null);
        }

        protected override Dictionary<string, Action<string[]>> RegisterSubcommands()
        {
            return new Dictionary<string, Action<string[]>>()
            {
                { "room", LoadRoom },
            };
        }
    }
}
