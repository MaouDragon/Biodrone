using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wall:MonoBehaviour, ICollidable {
	public float speedChange=0.95f;
	public float health=0;

	// Use this for initialization
	public virtual void Start() {
		if (health==0)
			health = float.PositiveInfinity;
	}

	// Update is called once per frame
	void Update() {

	}

	public virtual void Hit(RaycastHit rayHit, Bullet bullet) {
		if (TryBreak(rayHit, bullet))
			return;
		CreateBullet(rayHit, bullet.GetType(), BulletRemainVel(rayHit, bullet), BulletVelocity(rayHit, bullet), bullet.canSplit, bullet.numHits);
	}

	protected virtual bool TryBreak(RaycastHit rayHit, Bullet bullet) {
		health -= bullet.vel.magnitude/12;
		if (health<=0) {
			CreateBullet(rayHit, bullet.GetType(), Vector3.zero, bullet.vel, bullet.canSplit, bullet.numHits);
			Destroy(gameObject);
			return true;
		}
		return false;
	}

	protected void CreateBullet(RaycastHit rayHit, Type type, Vector3 bulletRemainVel, Vector3 bulletVelocity, bool canSplit, int numHits) {
		Bullet bullet = Bullet.CreateNewBullet(rayHit.point, bulletVelocity/**speedChange*/, type);
		if (bullet!=null) {
			bullet.canSplit = canSplit;
			bullet.numHits = numHits+1;
			//bullet.Move(bulletRemainVel);
		}
	}

	protected virtual Vector3 BulletNormal(RaycastHit rayHit) {
		return FixVector3(rayHit.normal);
	}

	protected virtual Vector3 BulletRemainVel(RaycastHit rayHit, Bullet bullet, Vector3 dir=default(Vector3)) {
		// adjust dir, as necessary
		if (dir==default(Vector3))
			dir = BulletVelocity(rayHit, bullet);
		dir = dir.magnitude*bullet.vel;

		// return the new position
		float totalDist = bullet.vel.magnitude*Time.fixedDeltaTime;
		return FixVector3((totalDist-rayHit.distance)*dir);
	}

	protected virtual Vector3 BulletVelocity(RaycastHit rayHit, Bullet bullet) {
		// perform the reflection
		Vector3 normal = BulletNormal(rayHit);
		normal = new Vector3(normal.x, normal.y, 0).normalized;
		Vector3 newDir = Vector3.Reflect(bullet.vel, normal);
		return FixVector3(newDir.normalized*bullet.vel.magnitude);
	}

	protected Vector3 FixVector3(Vector3 vec) { return new Vector3(vec.x, vec.y, 0); }
}