using Il2CppTGK.Game.Components.Persistence;
using Il2CppTGK.Game.Components;
using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class HitboxComponent : MonoBehaviour
    {
        private const float LINE_WIDTH = 0.03f;

        private BoxCollider2D collider;

        private SpriteRenderer top;
        private SpriteRenderer left;
        private SpriteRenderer right;
        private SpriteRenderer bottom;

        public void Setup(BoxCollider2D collider)
        {
            this.collider = collider;
            GameObject obj;

            float xPos = collider.offset.x;
            float yPos = collider.offset.y;
            float lineWidth = LINE_WIDTH / collider.transform.localScale.x;
            float lineHeight = LINE_WIDTH / collider.transform.localScale.y;
            float colliderHalfWidth = collider.size.x / 2;
            float colliderHalfHeight = collider.size.y / 2;

            obj = new GameObject("TOP");
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(xPos - colliderHalfWidth, yPos + colliderHalfHeight, 0);
            obj.transform.localScale = new Vector3(collider.size.x + LINE_WIDTH, lineHeight, 0);
            top = obj.AddComponent<SpriteRenderer>();
            top.sprite = Main.CheatConsole.HitboxViewer.HitboxImage;

            obj = new GameObject("LEFT");
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(xPos - colliderHalfWidth, yPos - colliderHalfHeight, 0);
            obj.transform.localScale = new Vector3(lineWidth, collider.size.y, 0);
            left = obj.AddComponent<SpriteRenderer>();
            left.sprite = Main.CheatConsole.HitboxViewer.HitboxImage;

            obj = new GameObject("RIGHT");
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(xPos + colliderHalfWidth, yPos - colliderHalfHeight, 0);
            obj.transform.localScale = new Vector3(lineWidth, collider.size.y, 0);
            right = obj.AddComponent<SpriteRenderer>();
            right.sprite = Main.CheatConsole.HitboxViewer.HitboxImage;

            obj = new GameObject("BOTTOM");
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(xPos - colliderHalfWidth, yPos - colliderHalfHeight, 0);
            obj.transform.localScale = new Vector3(collider.size.x + LINE_WIDTH, lineHeight, 0);
            bottom = obj.AddComponent<SpriteRenderer>();
            bottom.sprite = Main.CheatConsole.HitboxViewer.HitboxImage;

            UpdateColors();
        }

        public void UpdateColors()
        {
            Color color;
            int order;
            if (!Main.CheatConsole.HitboxViewer.HitboxConfig.useColor)
            {
                color = Color.green;
                order = 0;
            }
            else if (!collider.isActiveAndEnabled)
            {
                color = Color.gray;
                order = 20;
            }
            else if (collider.transform.HasComponentInParent<PlayerPersistentComponent>())
            {
                color = Color.cyan;
                order = 100;
            }
            else if (collider.transform.HasComponentInParent<AliveEntity>())
            {
                color = Color.red;
                order = 80;

            }
            else if (collider.isTrigger)
            {
                color = Color.green;
                order = 60;
            }
            else
            {
                color = Color.yellow;
                order = 40;
            }

            top.color = color;
            //top.sortingLayerName = "Foreground Parallax 1";
            top.sortingOrder = order;
            left.color = color;
            //left.sortingLayerName = "Foreground Parallax 1";
            left.sortingOrder = order;
            right.color = color;
            //right.sortingLayerName = "Foreground Parallax 1";
            right.sortingOrder = order;
            bottom.color = color;
            //bottom.sortingLayerName = "Foreground Parallax 1";
            bottom.sortingOrder = order;
        }
    }
}
