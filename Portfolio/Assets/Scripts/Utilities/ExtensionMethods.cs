using UnityEngine;

namespace Utilities
{
    public static class ExtensionMethods
    {
        public static Vector3 GetPolarDistance(this Vector3 initPos, float radius, float angle)
        {
            return initPos + new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
        }
    }
}
