using UnityEngine;
using System.Collections;

public class TripleWall : Wall
{
	public override void Hit(RaycastHit rayhit, Bullet bullet)
	{
		// create 3 new bullets
		Bullet.CreateNewBullet(
			ReflectedStartPos(rayhit, bullet, -bullet.vel),
			-bullet.vel);

		Bullet.CreateNewBullet(
			ReflectedStartPos(rayhit, bullet, ReflectedNormal(rayhit, bullet)),
			ReflectedNormal(rayhit, bullet));

		Bullet.CreateNewBullet(
			ReflectedStartPos(rayhit, bullet, ReflectedVelocity(rayhit, bullet)),
			ReflectedVelocity(rayhit, bullet));
	}
}
