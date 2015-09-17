using UnityEngine;
using System.Collections;

public class RandomWall : Wall
{
	protected override Vector3 BulletVelocity(RaycastHit rayHit, Bullet bullet) {
		Vector3 normal = BulletNormal(rayHit);
		Vector3 newDir = Random.insideUnitCircle;
		if (!new Plane(normal, 0).GetSide(newDir))
			newDir = -newDir;
		return FixVector3(newDir.normalized*bullet.vel.magnitude);
	}
}
