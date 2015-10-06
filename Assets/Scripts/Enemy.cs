using UnityEngine;
using System.Collections;

public class Enemy:MonoBehaviour, ICollidable {
	public float firePeriod=1;
	public float fireSpeed=12;
	public float rangeOfFire=20;
	public int health=0;
	private float timeToFire;
	public GameObject player;

	public virtual void Start() {
		timeToFire = Random.Range(0, firePeriod);
		if (health==0)
			health = int.MaxValue;
	}

	public virtual void Update() {
		timeToFire -= Time.deltaTime;
		if (timeToFire<0) {
			Fire();
			timeToFire = firePeriod;
		}
	}

	public virtual void Fire() {
		Vector3 dir = (player.transform.position-transform.position).normalized;
		float angle = (Random.Range(0, 1.0f)-Random.Range(0,1.0f)) * rangeOfFire;
		dir = Quaternion.AngleAxis(angle, Vector3.back)*dir;
		Bullet.CreateNewBullet(transform.position, dir*fireSpeed, typeof(EnemyBullet));
	}

	public void Hit(RaycastHit rayhit, Bullet bullet) {
        Bullet.CreateNewBullet(rayhit.point, bullet.vel, typeof(EnemyBullet));
        if (--health<=0)
			Destroy(gameObject);
	}
}
