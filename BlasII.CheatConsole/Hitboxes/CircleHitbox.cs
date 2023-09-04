using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class CircleHitbox : AbstractHitbox
    {
        public void SetupCircle(CircleCollider2D collider)
        {
            float xPos = collider.offset.x;
            float yPos = collider.offset.y;
            float lineWidth = LINE_WIDTH / collider.transform.localScale.x;
            float lineHeight = LINE_WIDTH / collider.transform.localScale.y;
            float colliderHalfWidth = collider.radius;
            float colliderHalfHeight = collider.radius;

            CreateLine("TOP",
                new Vector2(xPos - colliderHalfWidth, yPos + colliderHalfHeight),
                new Vector2(collider.radius * 2 + LINE_WIDTH, lineHeight));

            CreateLine("LEFT",
                new Vector2(xPos - colliderHalfWidth, yPos - colliderHalfHeight),
                new Vector2(lineWidth, collider.radius * 2));

            CreateLine("RIGHT",
                new Vector2(xPos + colliderHalfWidth, yPos - colliderHalfHeight),
                new Vector2(lineWidth, collider.radius * 2));

            CreateLine("BOTTOM",
                new Vector2(xPos - colliderHalfWidth, yPos - colliderHalfHeight),
                new Vector2(collider.radius * 2 + LINE_WIDTH, lineHeight));

            StoreCollider(collider);
            UpdateColors();
        }
    }
}
