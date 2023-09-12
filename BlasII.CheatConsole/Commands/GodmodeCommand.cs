using BlasII.ModdingAPI.Storage;

namespace BlasII.CheatConsole.Commands
{
    internal class GodmodeCommand : BaseCommand
    {
        public GodmodeCommand() : base("godmode") { }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "on":
                    Write("Activating godmode");
                    _active = true;
                    break;
                case "off":
                    Write("Deactivating godmode");
                    _active = false;
                    break;
                default:
                    WriteFailure("Acceptable input is 'on' or 'off'");
                    break;
            }
        }

        public override void Update()
        {
            if (_active && Main.CheatConsole.LoadStatus.GameSceneLoaded)
            {
                if (StatStorage.TryGetRangeStat("Health", out var health))
                {
                    StatStorage.PlayerStats.SetCurrentToMax(health);
                }
                if (StatStorage.TryGetRangeStat("Fervour", out var fervour))
                {
                    StatStorage.PlayerStats.SetCurrentToMax(fervour);
                }
            }
        }

        private bool _active;
    }
}
