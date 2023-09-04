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
        }

        public void UpdateColors()
        {
            Color color;
            if (!collider.isActiveAndEnabled)
                color = Color.gray;
            else if (collider.transform.HasComponentInParent<PlayerPersistentComponent>())
                color = Color.cyan;
            else if (collider.transform.HasComponentInParent<AliveEntity>())
                color = Color.red;
            else if (collider.isTrigger)
                color = Color.green;
            else
                color = Color.yellow;

            top.color = color;
            left.color = color;
            right.color = color;
            bottom.color = color;
        }
    }
}
