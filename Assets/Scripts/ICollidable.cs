using UnityEngine;
using System.Collections;

public interface ICollidable
{
    public void Hit(RaycastHit rayhit, Bullet bullet);
}
