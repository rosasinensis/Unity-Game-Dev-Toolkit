using UnityEngine;

namespace MKUtil
{
    public static class Calc
    {

        // DirectionTo: Gets direction
        // DistanceTo: Gets distance
        // IsWithinRange: Returns a bool if within some range

        // Just the Vector3s here.
        public static Vector3 DirectionTo(this Vector3 self, Vector3 target)
        {
            return (target - self).normalized;
        }
        public static float DistanceTo(this Vector3 self, Vector3 target)
        {
            return (self - target).sqrMagnitude;
        }
        public static bool IsWithinRange(this Vector3 self, Vector3 target, float range)
        {
            return self.DistanceTo(target) <= range * range;
        }


        // ! NOTE: Only transforms for self for the Dot product and IsFacing,
        // since Vector3s don't have a "forward".

        // Dot: Gets the dot product of a transform's forward and a target.
        // 1 = directly in front, 0 = to the side, -1 = directly behind.

        public static float Dot(this Transform self, Vector3 target)
        {

            return Vector3.Dot(self.forward, self.position.DirectionTo(target));
        }


        // IsFacing: Provide a 'threshold' for the something like the 'field of view' of some transform self and its target.
        // 1.0 = directly in front, 0.0 = everything in 180 degrees

        public static bool IsFacing(this Transform self, Transform target, float threshold = 0.5f)
        {
            return self.Dot(target.position) >= threshold;
        }
        public static bool IsFacing(this Transform self, Vector3 target, float threshold = 0.5f)
        {
            return self.Dot(target) >= threshold;
        }

        // GetLookAtRotation: Gets quaternion rotation.

        public static Quaternion GetLookAtRotation(this Vector3 self, Vector3 target)
        {
            Vector3 direction = self.DirectionTo(target);
            return direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);
        }


        // --- Transform Shortcuts ---

        public static Vector3 DirectionTo(this Transform self, Transform target) => self.position.DirectionTo(target.position);
        public static float DistanceTo(this Transform self, Transform target) => self.position.DistanceTo(target.position);

        public static float Dot(this Transform self, Transform target) => self.Dot(target.position);

        public static Quaternion GetLookAtRotation(this Transform self, Vector3 target) => self.position.GetLookAtRotation(target);

    }

}