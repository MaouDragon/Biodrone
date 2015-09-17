using UnityEngine;
using System.Collections;

public class NormalWall : Wall
{
	protected override Vector3 ReflectedVelocity(RaycastHit rayHit, Bullet bullet)
	{
		return ReflectedNormal(rayHit, bullet).normalized*bullet.vel.magnitude;
	}
}