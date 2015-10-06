using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Bullet : MonoBehaviour//, ICollidable
{
	public Vector3 vel;
    public bool canSplit = true;
	public int numHits;
	private static int maxBullets=500;
	private static int curBullets=0;
	private static List<Bullet> bullets=new List<Bullet>();
	public static GameObject playerBullet;
	public static GameObject normalBullet;
	public static Material bulletTrail;

	// Use this for initialization
	public void Move(Vector3 diff) {
		// raycast
		List<RaycastHit> hits = new List<RaycastHit>(Physics.RaycastAll(transform.position, diff, diff.magnitude));

		// loop over raycast hits, in order from the closes to the nearest
		hits.Sort((a, b) => (a.distance>=b.distance?1:-1));
		for (int i=0; i<hits.Count; ++i) {
			// find an ICollidable
			ICollidable collidable = FindICollidable(hits[i].transform.gameObject);
			if (collidable!=null) {
				Destroy(gameObject);
				collidable.Hit(hits[i], this);
				return;
			}
		}

		// move the bullet
		transform.position += diff;
	}

	void OnTriggerEnter(Collider other) {
		ICollidable collidable = FindICollidable(other.gameObject);
		if (collidable!=null && collidable.GetType()==typeof(Player)) {
			collidable.Hit(new RaycastHit(), this);
			Destroy(gameObject);
			return;
		}
	}
	
	void FixedUpdate () {
		Move(Time.fixedDeltaTime*vel);
		transform.LookAt(transform.position+Vector3.back, vel);
	}

	private ICollidable FindICollidable(GameObject g) {
		Component[] comps = g.GetComponents<Component>();
		for (int j=0; j<comps.Length; ++j)
			if (comps[j] is ICollidable)
				return comps[j] as ICollidable;
		return null;
	}

    public static Bullet CreateNewBullet(Vector3 startPos, Vector3 vel, Type type=null) {
        // If no bullet type was specified, standard Bullet type is used
        if (type == null)
            type = typeof(Bullet);

        // handle bullet limit
		//print("bullet type: "+type);
		if (curBullets>=maxBullets && type!=typeof(PlayerBullet))
			return null;
		++curBullets;
		
		// create the GameObject and Bullet
		//GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		GameObject obj = (GameObject)Instantiate(type==typeof(PlayerBullet)?playerBullet:normalBullet);
		Bullet bullet = obj.AddComponent(type) as Bullet;

		// initialize the Bullet
		bullet.transform.position = new Vector3(startPos.x, startPos.y);
		bullet.transform.localScale = Vector3.one*0.6f;
		bullet.vel = new Vector3(vel.x, vel.y)*12f/14f;
		bullet.GetComponent<Collider>().isTrigger = true;
		
		// initialize the TrailRenderer
		TrailRenderer tr = obj.AddComponent<TrailRenderer>();
		tr.time = 0.05f;
		tr.startWidth = tr.endWidth = 0.1f;
		tr.material = (Material)Instantiate(bulletTrail);
		tr.material.color = (type==typeof(PlayerBullet)?new Color(10/255f, 157/255f, 0):new Color(157/255f, 0, 0));
		//tr.sharedMaterial = bulletTrail;

		bullets.Add(bullet);
		return bullet;
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }

	public void OnDestroy() { --curBullets; }

	public static void MoveCollidables(GameObject[] objs, Vector3 diff) {
		throw new NotImplementedException("MoveCollidables isn't implemented yet");
		List<Bullet> tmpBullets = new List<Bullet>(bullets);
		for (int i=0; i<tmpBullets.Count; ++i)
			try { tmpBullets[i].transform.position += diff; } catch (Exception e) { if (typeof(MissingReferenceException)!=e.GetType()) throw e; }
		for (int i=0; i<objs.Length; ++i)
			objs[i].transform.position += diff;
		for (int i=0; i<tmpBullets.Count; ++i)
			try { tmpBullets[i].Move(-diff); } catch (Exception e) { if (typeof(MissingReferenceException)!=e.GetType()) throw e; }
	}

	public static void RotateCollidables(GameObject collidables, Vector3 center, float angleInRadians) {

	}
}
