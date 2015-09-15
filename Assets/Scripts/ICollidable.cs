using UnityEngine;
using System.Collections;

public interface ICollidable
{
    void Hit(RaycastHit rayhit, Bullet bullet);
}
