using BlasII.ModdingAPI.Storage;
using Il2CppTGK.Game.Components.StatsSystem.Data;

namespace BlasII.CheatConsole.Commands
{
    internal class HealthCommand : BaseCommand
    {
        public HealthCommand() : base("health") { }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "set":
                    {
                        if (!ValidateParameterCount(args, 2))
                            return;

                        if (!ValidateIntParamater(args[1], out int amount))
                            return;

                        SetHealth(amount);
                        break;
                    }
                case "fill":
                    {
                        if (!ValidateParameterCount(args, 1))
                            return;

                        FillHealth();
                        break;
                    }
                case "current":
                    {
                        if (!ValidateParameterCount(args, 1))
                            return;

                        CurrentHealth();
                        break;
                    }
                default:
                    {
                        WriteFailure("Unknown subcommand: " + args[0]);
                        break;
                    }
            }
        }

        private void SetHealth(int amount)
        {

        }

        private void FillHealth()
        {
            if (StatStorage.TryGetRangeStat("Health", out RangeStatID health))
            {
                Write("Filling health");
                StatStorage.PlayerStats.SetCurrentToMax(health);
            }
        }

        private void CurrentHealth()
        {

        }
    }
}
