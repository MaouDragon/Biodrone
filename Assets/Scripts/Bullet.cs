using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Bullet : MonoBehaviour//, ICollidable
{
	public Vector3 vel;
	//private static int maxBullets=500;
	//private static int curBullets=0;
	private static Dictionary<Type, int> maxBullets=new Dictionary<Type,int>();
	private static Dictionary<Type, int> curBullets=new Dictionary<Type,int>();

	public void Start() {
		maxBullets.Add(typeof(Bullet), 500);
		maxBullets.Add(typeof(PlayerBullet), 1);
		
		curBullets.Add(typeof(Bullet), 0);
		curBullets.Add(typeof(PlayerBullet), 0);
	}

	// Use this for initialization
	public void Move(Vector3 diff) {
		// raycast
		List<RaycastHit> hits = new List<RaycastHit>(Physics.RaycastAll(transform.position, diff, diff.magnitude));

		// loop over raycast hits, in order from the closes to the nearest
		hits.Sort((a, b) => (a.distance>=b.distance?1:-1));
		for (int i=0; i<hits.Count; ++i) {
			// find an ICollidable
			Component[] comps = hits[i].transform.gameObject.GetComponents<Component>();
			for (int j=0; j<comps.Length; ++j) {
				if (comps[j] is ICollidable) {
					(comps[j] as ICollidable).Hit(hits[i], this);
					DestroyImmediate(gameObject);
					return;
				}
			}
		}

		// move the bullet
		transform.position += diff;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move(Time.fixedDeltaTime*vel);
	}

    public static Bullet CreateNewBullet(Vector3 startPos, Vector3 vel, Type type=null) {
        // If no bullet type was specified, standard Bullet type is used
        if (type == null)
            type = typeof(Bullet);

        // handle bullet limit
        if (curBullets[type]>=maxBullets[type])
			return null;
		curBullets[type] = curBullets[type]+1;
		
		// create the GameObject and Bullet
		GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		Bullet bullet = obj.AddComponent(type) as Bullet;

		// initialize the Bullet
		bullet.transform.position = new Vector3(startPos.x, startPos.y);
		bullet.transform.localScale = Vector3.one*0.1f;
		bullet.vel = new Vector3(vel.x, vel.y);
		bullet.GetComponent<Collider>().isTrigger = true;
		return bullet;
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }

	public void OnDestroy() { curBullets[this.GetType()] = curBullets[this.GetType()]-1; }
}
