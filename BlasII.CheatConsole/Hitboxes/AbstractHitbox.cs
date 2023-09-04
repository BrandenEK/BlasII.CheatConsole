using Il2CppTGK.Game.Components.Persistence;
using Il2CppTGK.Game.Components;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public abstract class AbstractHitbox : MonoBehaviour
    {
        protected const float LINE_WIDTH = 0.03f;

        private Collider2D _collider;
        private readonly List<SpriteRenderer> _renderers = new();

        protected void StoreCollider(Collider2D collider) => _collider = collider;

        protected void AddRenderer(SpriteRenderer renderer) => _renderers.Add(renderer);

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

            foreach (SpriteRenderer sr in _renderers)
            {
                sr.color = color;
                //sr.sortingLayerName = "Foreground Parallax 1";
                sr.sortingOrder = order;
            }
        }

        protected void CreateLine(string name, Vector2 position, Vector2 scale)
        {
            var obj = new GameObject(name);
            obj.transform.parent = transform;
            obj.transform.localPosition = position;
            obj.transform.localScale = scale;
            var sr = obj.AddComponent<SpriteRenderer>();
            sr.sprite = Main.CheatConsole.HitboxViewer.HitboxImage;
            AddRenderer(sr);
        }
    }
}
