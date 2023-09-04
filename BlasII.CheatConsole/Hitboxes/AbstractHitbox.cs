using Il2CppTGK.Game.Components.Persistence;
using Il2CppTGK.Game.Components;
using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public abstract class AbstractHitbox : MonoBehaviour
    {
        private Collider2D _collider;
        private LineRenderer _lineRenderer;

        protected void StoreCollider(Collider2D collider) => _collider = collider;
        protected void StoreRenderer(LineRenderer renderer) => _lineRenderer = renderer;

        public void UpdateColors()
        {
            Color color;
            int order;
            if (!Main.CheatConsole.HitboxViewer.HitboxConfig.useColor)
            {
                color = Color.green;
                order = 0;
            }
            else if (!_collider.isActiveAndEnabled)
            {
                color = Color.gray;
                order = 20;
            }
            else if (_collider.transform.HasComponentInParent<PlayerPersistentComponent>())
            {
                color = Color.cyan;
                order = 100;
            }
            else if (_collider.transform.HasComponentInParent<AliveEntity>())
            {
                color = Color.red;
                order = 80;

            }
            else if (_collider.isTrigger)
            {
                color = Color.green;
                order = 60;
            }
            else
            {
                color = Color.yellow;
                order = 40;
            }

            _lineRenderer.SetColors(color, color);
            _lineRenderer.sortingOrder = order;
        }
    }
}
