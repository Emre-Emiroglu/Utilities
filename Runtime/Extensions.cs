using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// Extension methods for transforming the position and rotation of a Transform.
    /// </summary>
    public static class Extensions
    {
        #region Executes
        /// <summary>
        /// Sets the axes (x, y, z) of a Transform's position.
        /// </summary>
        /// <param name="transform">The Transform to modify.</param>
        /// <param name="x">The x-coordinate to set. If null, the current x value is retained.</param>
        /// <param name="y">The y-coordinate to set. If null, the current y value is retained.</param>
        /// <param name="z">The z-coordinate to set. If null, the current z value is retained.</param>
        /// <param name="local">Indicates whether the position should be in local space (true) or world space (false).</param>
        /// <returns>The modified Transform.</returns>
        public static Transform SetAxes(this Transform transform, float? x = null, float? y = null, float? z = null,
            bool local = false)
        {
            Vector3 pos = local ? transform.localPosition : transform.position;

            if (x != null)
                pos.x = x.Value;
            if (y != null)
                pos.y = y.Value;
            if (z != null)
                pos.z = z.Value;

            if (local)
                transform.localPosition = pos;
            else
                transform.position = pos;

            return transform;
        }
        
        /// <summary>
        /// Rotates a Transform to look at a target, with an optional axis and angle offset.
        /// </summary>
        /// <param name="transform">The Transform to modify.</param>
        /// <param name="target">The target Transform to look at.</param>
        /// <param name="axis">The axis around which to rotate.</param>
        /// <param name="angleOffset">The optional angle offset to apply after looking at the target.</param>
        public static void LookAtWithAxis(this Transform transform, Transform target, Vector3 axis,
            float angleOffset = 0f)
        {
            transform.LookAt(target);
            transform.Rotate(axis, angleOffset, Space.Self);
        }
        
        /// <summary>
        /// Gradually rotates a Transform to look at a target, with a maximum radians delta per frame.
        /// </summary>
        /// <param name="transform">The Transform to modify.</param>
        /// <param name="target">The target Transform to look at.</param>
        /// <param name="axis">The axis around which to rotate.</param>
        /// <param name="maxRadiansDelta">The maximum radians the rotation can change per frame.</param>
        /// <param name="stableUpVector">Indicates whether to maintain a stable up vector during rotation.</param>
        public static void LookAtGradually(this Transform transform, Transform target, Vector3 axis,
            float maxRadiansDelta, bool stableUpVector = false)
        {
            Vector3 dir = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, maxRadiansDelta, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDir);
            if (stableUpVector)
                transform.rotation = Quaternion.Euler(axis.normalized * transform.rotation.eulerAngles.magnitude);
        }
        
        /// <summary>
        /// Recursively finds a child Transform by name.
        /// </summary>
        /// <param name="transform">The parent Transform to start the search from.</param>
        /// <param name="name">The name of the child Transform to find.</param>
        /// <param name="includeInactive">Indicates whether to include inactive children in the search.</param>
        /// <returns>The found Transform, or null if not found.</returns>
        public static Transform FindRecursive(this Transform transform, string name, bool includeInactive = false)
        {
            foreach (Transform child in transform.GetComponentsInChildren<Transform>(includeInactive))
            {
                if (child.name.Equals(name))
                    return child;
            }
            return null;
        }
        #endregion
    }
}