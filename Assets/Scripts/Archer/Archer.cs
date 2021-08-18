using System;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;

    public void TakeArrows(int count)
    {
        if(count <= 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        _quiver.Add(count);
    }
}
