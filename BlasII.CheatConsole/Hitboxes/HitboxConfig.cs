
namespace BlasII.CheatConsole.Hitboxes
{
    public class HitboxConfig
    {
        public readonly bool useColor;
        public readonly bool showGeometry;
        public readonly float updateDelay;

        public HitboxConfig(bool useColor, bool showGeometry, float updateDelay)
        {
            this.useColor = useColor;
            this.showGeometry = showGeometry;
            this.updateDelay = updateDelay;
        }
    }
}
