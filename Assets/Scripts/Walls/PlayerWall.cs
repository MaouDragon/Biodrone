using UnityEngine;
using System.Collections;

public class PlayerWall : Wall {
	public override void Start() {
		base.Start();
		this.speedChange = 1;
	}

	public override void Hit(RaycastHit rayHit, Bullet bullet) {
		Vector3 vel = BulletVelocity(rayHit, bullet);
		vel = ((-0.8f*transform.forward.normalized) + (0.2f*vel.normalized)) * vel.magnitude;
		CreateBullet(rayHit, typeof(PlayerBullet), BulletRemainVel(rayHit, bullet), vel, bullet.canSplit, bullet.numHits);
	}
}
