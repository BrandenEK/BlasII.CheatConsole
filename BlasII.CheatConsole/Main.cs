using MelonLoader;

namespace BlasII.CheatConsole
{
    public class Main : MelonMod
    {
        public static CheatConsole CheatConsole { get; private set; }

        public override void OnLateInitializeMelon()
        {
            CheatConsole = new CheatConsole();
        }
    }
}