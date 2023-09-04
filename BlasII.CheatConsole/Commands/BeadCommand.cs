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

            Write("Adding bead: " + parameters[0]);
        }

        private void RemoveBead(string[] parameters)
        {
            if (!ValidateParameterCount("remove", parameters, 1))
                return;

            Write("Removing bead: " + parameters[0]);
        }

        protected override Dictionary<string, Action<string[]>> RegisterSubcommands()
        {
            return new Dictionary<string, Action<string[]>>()
            {
                { "add", AddBead },
                { "remove", RemoveBead },
            };
        }
    }
}
