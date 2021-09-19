using UnityEngine;

public static class Vector3Extensions
{
    public static bool SqrDistance(this Vector3 offset, Transform self, Transform target, float distance)
    { 
        offset = self.position - target.transform.position;
        float sqrLength = offset.sqrMagnitude;

        return sqrLength < distance * distance;
    }
}
