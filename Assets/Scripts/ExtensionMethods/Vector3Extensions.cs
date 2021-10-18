using UnityEngine;

public static class Vector3Extensions
{
    public static bool SqrDistance(this Vector3 offset, Transform self, Transform target, float distance)
    { 
        offset = target.transform.position - self.position;
        float sqrLength = offset.sqrMagnitude;

        return sqrLength < distance * distance;
    }
}
