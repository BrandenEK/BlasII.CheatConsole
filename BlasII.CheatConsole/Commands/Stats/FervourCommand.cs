using BlasII.ModdingAPI.Storage;
using Il2CppTGK.Game.Components.StatsSystem.Data;

namespace BlasII.CheatConsole.Commands
{
    internal class FervourCommand : BaseCommand
    {
        public FervourCommand() : base("fervour") { }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "fill":
                    {
                        if (!ValidateParameterCount(args, 1))
                            return;

                        FillFervour();
                        break;
                    }
                default:
                    {
                        WriteFailure("Unknown subcommand: " + args[0]);
                        break;
                    }
            }
        }

        private void FillFervour()
        {
            if (StatStorage.TryGetRangeStat("Fervour", out RangeStatID fervour))
            {
                Write("Filling fervour");
                StatStorage.PlayerStats.SetCurrentToMax(fervour);
            }
        }
    }
}
