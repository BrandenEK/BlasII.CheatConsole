using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class BoxHitbox : AbstractHitbox
    {
        public void SetupBox(BoxCollider2D collider)
        {
            if (collider.size.x >= 15 || collider.size.y >= 15)
            {
                // Hide boxes that are really large
                StoreCollider(collider);
                return;
            }

            float xPos = collider.offset.x;
            float yPos = collider.offset.y;
            float lineWidth = LINE_WIDTH / collider.transform.localScale.x;
            float lineHeight = LINE_WIDTH / collider.transform.localScale.y;
            float colliderHalfWidth = collider.size.x / 2;
            float colliderHalfHeight = collider.size.y / 2;

            CreateLine("TOP",
                new Vector2(xPos - colliderHalfWidth, yPos + colliderHalfHeight),
                new Vector2(collider.size.x + LINE_WIDTH, lineHeight));

            CreateLine("LEFT",
                new Vector2(xPos - colliderHalfWidth, yPos - colliderHalfHeight),
                new Vector2(lineWidth, collider.size.y));

            CreateLine("RIGHT",
                new Vector2(xPos + colliderHalfWidth, yPos - colliderHalfHeight),
                new Vector2(lineWidth, collider.size.y));

            CreateLine("BOTTOM",
                new Vector2(xPos - colliderHalfWidth, yPos - colliderHalfHeight),
                new Vector2(collider.size.x + LINE_WIDTH, lineHeight));

            StoreCollider(collider);
            UpdateColors();
        }
    }
}
