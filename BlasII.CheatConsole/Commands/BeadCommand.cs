using BlasII.ModdingAPI.Storage;
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

            string beadId = parameters[0].ToUpper();

            if (!ItemStorage.TryGetRosaryBead(beadId, out var bead))
            {
                Write($"Bead {beadId} does not exist!");
                return;
            }

            Write("Adding bead: " + beadId);
            ItemStorage.PlayerInventory.AddItemAsync(bead);
        }

        private void RemoveBead(string[] parameters)
        {
            if (!ValidateParameterCount("remove", parameters, 1))
                return;

            string beadId = parameters[0].ToUpper();

            if (!ItemStorage.TryGetRosaryBead(beadId, out var bead))
            {
                Write($"Bead {beadId} does not exist!");
                return;
            }

            Write("Removing bead: " + beadId);
            ItemStorage.PlayerInventory.RemoveItem(bead);
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
