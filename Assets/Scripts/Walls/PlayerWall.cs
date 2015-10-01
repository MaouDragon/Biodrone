using UnityEngine;
using System.Collections;

public class PlayerWall : Wall {
	public override void Hit(RaycastHit rayHit, Bullet bullet) {
		CreateBullet(rayHit, typeof(PlayerBullet), BulletRemainVel(rayHit, bullet), BulletVelocity(rayHit, bullet), bullet.canSplit, bullet.numHits);
	}
}
