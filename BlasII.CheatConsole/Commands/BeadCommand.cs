using System;
using System.Collections.Generic;

namespace BlasII.CheatConsole.Commands
{
    internal class BeadCommand : BaseCommand
    {
        public BeadCommand() : base("bead") { }

        private void AddBead(string[] parameters)
        {
            if (!ValidateParameterCount("add", parameters, 1))
                return;

            Main.CheatConsole.LogWarning("Adding bead: " + parameters[0]);
        }

        private void RemoveBead(string[] parameters)
        {
            if (!ValidateParameterCount("remove", parameters, 1))
                return;

            Main.CheatConsole.LogWarning("Removing bead: " + parameters[0]);
        }

        protected override Dictionary<string, Action<string[]>> RegisterCommands()
        {
            return new Dictionary<string, Action<string[]>>()
            {
                { "add", AddBead },
                { "remove", RemoveBead },
            };
        }
    }
}
