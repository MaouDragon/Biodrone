using UnityEngine;
using System.Collections;

public class ReverseWall : Wall
{
	protected override Vector3 BulletVelocity(RaycastHit rayHit, Bullet bullet)
	{
		return -bullet.vel;
	}
}
