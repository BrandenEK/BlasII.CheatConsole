using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class CircleHitbox : AbstractHitbox
    {
        public void SetupCircle(CircleCollider2D collider, LineRenderer renderer)
        {
            if (collider.radius >= 5)
            {
                // Hide circles that are really large
                StoreCollider(collider);
                StoreRenderer(renderer);
                return;
            }

            int steps = 100;
            float radius = collider.radius;

            renderer.positionCount = steps;
            for (int currentStep = 0; currentStep < steps; currentStep++)
            {
                float circumferenceProgress = (float)currentStep / (steps - 1);

                float currentRadian = circumferenceProgress * 2 * Mathf.PI;

                float xScaled = Mathf.Cos(currentRadian);
                float yScaled = Mathf.Sin(currentRadian);

                var currentPosition = new Vector2(radius * xScaled, radius * yScaled);
                renderer.SetPosition(currentStep, collider.offset + currentPosition);
            }

            StoreCollider(collider);
            StoreRenderer(renderer);
            UpdateColors();
        }
    }
}
