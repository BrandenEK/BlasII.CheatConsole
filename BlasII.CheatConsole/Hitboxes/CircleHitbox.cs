using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class CircleHitbox : AbstractHitbox
    {
        public void SetupCircle(CircleCollider2D collider)
        {
            if (collider.radius >= 5)
            {
                // Hide circles that are really large
                StoreCollider(collider);
                return;
            }

            float xPos = collider.offset.x;
            float yPos = collider.offset.y;
            float lineWidth = LINE_WIDTH / collider.transform.localScale.x;
            float lineHeight = LINE_WIDTH / collider.transform.localScale.y;
            float colliderFullRadius = collider.radius;
            float colliderHalfRadius = collider.radius / 2;
            float colliderQuarterRadius = collider.radius / 4;

            CreateLine("TOP",
                new Vector2(xPos - colliderHalfRadius, yPos + colliderFullRadius),
                new Vector2(collider.radius, lineHeight));

            CreateLine("LEFT",
                new Vector2(xPos - colliderFullRadius, yPos - colliderHalfRadius),
                new Vector2(lineWidth, collider.radius));

            CreateLine("RIGHT",
                new Vector2(xPos + colliderFullRadius, yPos - colliderHalfRadius),
                new Vector2(lineWidth, collider.radius));

            CreateLine("BOTTOM",
                new Vector2(xPos - colliderHalfRadius, yPos - colliderFullRadius),
                new Vector2(collider.radius, lineHeight));

            CreateLine("TOPLEFT",
                new Vector2(xPos - colliderFullRadius + lineWidth, yPos + colliderHalfRadius),
                new Vector2(colliderHalfRadius + colliderQuarterRadius - lineWidth, lineHeight),
                45);

            CreateLine("TOPRIGHT",
                new Vector2(xPos + colliderFullRadius + lineWidth, yPos + colliderHalfRadius),
                new Vector2(colliderHalfRadius + colliderQuarterRadius, lineHeight),
                135);

            CreateLine("BOTTOMLEFT",
                new Vector2(xPos - colliderHalfRadius + lineWidth, yPos - colliderFullRadius),
                new Vector2(colliderHalfRadius + colliderQuarterRadius, lineHeight),
                135);

            CreateLine("BOTTOMRIGHT",
                new Vector2(xPos + colliderHalfRadius + lineWidth, yPos - colliderFullRadius),
                new Vector2(colliderHalfRadius + colliderQuarterRadius - lineWidth, lineHeight),
                45);

            StoreCollider(collider);
            UpdateColors();
        }
    }
}
