using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wall : MonoBehaviour, ICollidable
{

	// Use this for initialization
	void Start () 
    {
		// extract and sort the points from the mesh
		Mesh m = GetComponent<MeshFilter>().mesh;
		HashSet<Vector3> points = new HashSet<Vector3>(m.vertices);
		List<Vector3> points2 = new List<Vector3>(points);
		points2.Sort((a, b) => {
			if (Mathf.Atan2(a.y, a.x) > Mathf.Atan2(b.y, b.x))
				return 1;
			return -1;
		});
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public virtual void Hit(RaycastHit rayHit, Bullet bullet) {
		//if (bullet.GetType()==typeof(Bullet))
			//return;
		// create a new bullet
		CreateBullet(rayHit, bullet.GetType(), BulletRemainVel(rayHit, bullet), BulletVelocity(rayHit, bullet), bullet.GetStopSplit());
    }

	protected void CreateBullet(RaycastHit rayHit, Type type, Vector3 bulletRemainVel, Vector3 bulletVelocity, bool stopSplit) {
		Bullet bullet = Bullet.CreateNewBullet(rayHit.point, bulletVelocity, type);
        if (stopSplit)
            bullet.SetStopSplit(true);
		//if (bullet!=null)
			//bullet.Move(bulletRemainVel);
	}

	protected virtual Vector3 BulletNormal(RaycastHit rayHit) {
		return FixVector3(rayHit.normal);
	}

	protected virtual Vector3 BulletRemainVel(RaycastHit rayHit, Bullet bullet, Vector3 dir=default(Vector3))
	{
		// adjust dir, as necessary
		if (dir==default(Vector3))
			dir = BulletVelocity(rayHit, bullet);
		dir = dir.magnitude*bullet.vel;

		// return the new position
		float totalDist = bullet.vel.magnitude*Time.fixedDeltaTime;
		return FixVector3((totalDist-rayHit.distance)*dir);
	}

	protected virtual Vector3 BulletVelocity(RaycastHit rayHit, Bullet bullet)
	{
		// perform the reflection
		Vector3 normal = BulletNormal(rayHit);
		normal = new Vector3(normal.x, normal.y, 0).normalized;
		Vector3 newDir = Vector3.Reflect(bullet.vel, normal);
		return FixVector3(newDir.normalized*bullet.vel.magnitude*1f);
	}

	protected Vector3 FixVector3(Vector3 vec) { return new Vector3(vec.x, vec.y, 0); }
}
