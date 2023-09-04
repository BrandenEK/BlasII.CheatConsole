using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class BoxHitbox : AbstractHitbox
    {
        public void SetupBox(BoxCollider2D collider, LineRenderer renderer)
        {
            if (collider.size.x >= 15 || collider.size.y >= 15)
            {
                // Hide boxes that are really large
                StoreCollider(collider);
                StoreRenderer(renderer);
                return;
            }

            Vector2 halfSize = collider.size / 2f;
            Vector2 topLeft = new(-halfSize.x, halfSize.y);
            Vector2 topRight = halfSize;
            Vector2 bottomRight = new(halfSize.x, -halfSize.y);
            Vector2 bottomLeft = -halfSize;
            Vector2[] points = new Vector2[]
            {
                topLeft, topRight, bottomRight, bottomLeft, topLeft
            };

            renderer.positionCount = 5;
            for (int i = 0; i < points.Length; i++)
            {
                renderer.SetPosition(i, collider.offset + points[i]);
            }

            StoreCollider(collider);
            StoreRenderer(renderer);
            UpdateColors();
        }
    }
}
