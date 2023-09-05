using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public static class HitboxExtensions
    {
        public static bool HasComponentInParent<T>(this Transform transform)
        {
            Transform parent = transform;
            while (parent != null)
            {
                if (parent.GetComponent<T>() != null)
                    return true;
                parent = parent.parent;
            }

            return false;
        }
    }
}
