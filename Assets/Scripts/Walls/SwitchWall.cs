using UnityEngine;
using System.Collections;

public class SwitchWall : Wall {
	public bool switchOn = false;
	public float meter = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Hit (RaycastHit rayHit, Bullet bullet)
	{
		if (bullet is PlayerBullet) 
		{
			meter -= bullet.vel.magnitude / 12;
			if (meter <= 0)
				Destroy (gameObject);
		}
		else
			CreateBullet(rayHit, bullet.GetType(), BulletRemainVel(rayHit, bullet), BulletVelocity(rayHit, bullet), bullet.canSplit, bullet.numHits);
	}
}
