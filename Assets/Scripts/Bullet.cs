using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Bullet : MonoBehaviour//, ICollidable
{
	public Vector3 vel;
    public bool canSplit = true;
	private static bool started=false;
	public int numHits;
	//private static int maxBullets=500;
	//private static int curBullets=0;
	private static Dictionary<Type, int> maxBullets=new Dictionary<Type,int>();
	private static Dictionary<Type, int> curBullets=new Dictionary<Type,int>();

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
				DestroyImmediate(gameObject);
				collidable.Hit(hits[i], this);
				return;
			}
		}

		// move the bullet
		transform.position += diff;
	}

	void OnTriggerEnter(Collider other) {
		/*ICollidable collidable = FindICollidable(other.gameObject);
		if (collidable!=null) {
			collidable.Hit(new RaycastHit(), this);
			DestroyImmediate(gameObject);
			return;
		}*/
	}
	
	void FixedUpdate () {
		Move(Time.fixedDeltaTime*vel);
	}

	void Update() {
		Material m = GetComponent<Renderer>().material;
		Color c = (vel.magnitude>6?Color.red:Color.black);
		m.color = c;
		print("color: "+c);
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

		// handle initial bullet limits
		if (!started) {
			started = true;

			maxBullets.Add(typeof(Bullet), 500);
			maxBullets.Add(typeof(PlayerBullet), 2);

			curBullets.Add(typeof(Bullet), 0);
			curBullets.Add(typeof(PlayerBullet), 0);
		}

        // handle bullet limit
		//print("bullet type: "+type);
		if (curBullets[type]>=maxBullets[type])
			return null;
		curBullets[type] = curBullets[type]+1;
		
		// create the GameObject and Bullet
		GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		Bullet bullet = obj.AddComponent(type) as Bullet;

		// initialize the Bullet
		bullet.transform.position = new Vector3(startPos.x, startPos.y);
		bullet.transform.localScale = Vector3.one*0.15f;
		bullet.vel = new Vector3(vel.x, vel.y);
		bullet.GetComponent<Collider>().isTrigger = true;
		return bullet;
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }

	public void OnDestroy() { curBullets[this.GetType()] = curBullets[this.GetType()]-1; }
}
