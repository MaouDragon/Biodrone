using UnityEngine;
using System.Collections;
using System;

public class MultiWall : Wall {
	public int numBullets=3;

	[Range(0.1f, 89.9f)]
	public float angle=5;

	public override void Start() {
		if (numBullets*angle>180)
			throw new Exception("Invalid Multi-Wall - range of bullets exceeds 180 degrees. Reduce the number of bullets OR reduce the angle until numBullets*angle < 180");
		base.Start();
	}

	public override void Hit(RaycastHit rayHit, Bullet bullet) {
		if (TryBreak(rayHit, bullet))
			return;

		// check if a vector is valid
		Func<Vector3, bool> isValid = (vec)=> {
			return Vector3.Angle(BulletNormal(rayHit), vec)<90;
		};

		// find the first direction
		Vector3[] directions = new Vector3[numBullets];
		directions[0] = BulletVelocity(rayHit, bullet);
		if (numBullets%2==0)
			directions[0] = Quaternion.AngleAxis(angle/2, Vector3.forward)*directions[0];
		if (!isValid(directions[0]))
			directions[0] = Quaternion.AngleAxis(-angle/2, Vector3.forward)*directions[0];

		// find the first side of directions
		Vector3 prev = directions[0];
		int i;
		for (i=1; i<1+numBullets/2; ++i) {
			prev = Quaternion.AngleAxis(angle, Vector3.forward)*prev;
			if (!isValid(prev))
				break;
			directions[i] = prev;
		}

		// find the second side of directions
		prev = directions[0];
		for (; i<numBullets; ++i) {
			prev = Quaternion.AngleAxis(-angle, Vector3.forward)*prev;
			if (!isValid(prev))
				break;
			directions[i] = prev;
		}

		// finish the first side, if necessary
		prev = directions[numBullets/2];
		for (; i<numBullets; ++i) {
			prev = Quaternion.AngleAxis(angle, Vector3.forward)*prev;
			if (!isValid(prev))
				throw new Exception("Invalid combination of bullets and angles");
			directions[i] = prev;
		}
		
		// create the bullets
		CreateBullet(rayHit, bullet.GetType(), BulletRemainVel(rayHit, bullet, directions[0]), directions[0], false, bullet.numHits);
        if (bullet.canSplit && (bullet.GetType()==typeof(PlayerBullet) || bullet.GetType()==typeof(EnemyBullet)))
        {
            for (i = 1; i < directions.Length; ++i)
            {
                //rayHit.point += BulletNormal(rayHit);
                CreateBullet(rayHit, bullet.GetType(), BulletRemainVel(rayHit, bullet, directions[i]), directions[i], false, bullet.numHits);
            }
        }
	}
}
