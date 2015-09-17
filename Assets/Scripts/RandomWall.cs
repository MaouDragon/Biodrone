using UnityEngine;
using System.Collections;

public class RandomWall : Wall
{
	protected override Vector3 ReflectedVelocity(RaycastHit rayHit, Bullet bullet) {
		Vector3 normal = ReflectedNormal(rayHit, bullet);
		Vector3 newDir = Random.insideUnitCircle;
		if (!new Plane(normal, 0).GetSide(newDir))
			newDir = -newDir;
		return newDir.normalized*bullet.vel.magnitude;
	}
}
