using BlasII.CheatConsole.Hitboxes;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;

namespace BlasII.CheatConsole
{
    public class Main : MelonMod
    {
        public static CheatConsole CheatConsole { get; private set; }

        public override void OnLateInitializeMelon()
        {
            if (!ClassInjector.IsTypeRegisteredInIl2Cpp<HitboxComponent>())
                ClassInjector.RegisterTypeInIl2Cpp<HitboxComponent>();

            CheatConsole = new CheatConsole();
        }
    }
}