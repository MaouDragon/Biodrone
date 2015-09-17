using UnityEngine;
using System.Collections;

public class NormalWall : Wall
{
	protected override Vector3 BulletVelocity(RaycastHit rayHit, Bullet bullet)
	{
		return rayHit.normal*bullet.vel.magnitude;
	}
}