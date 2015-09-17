using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour//, ICollidable
{
	public Vector3 vel;
	private static int maxBullets=500;
	private static int curBullets=0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
		// raycast
		Vector3 diff = Time.fixedDeltaTime*vel;
		RaycastHit result;
		bool hit = Physics.Raycast(transform.position, diff, out result, diff.magnitude);

		// if the raycast hit something
		if (hit)
		{
			// find an ICollidable
			Component[] comps = result.transform.gameObject.GetComponents<Component>();
			for (int i=0; i<comps.Length; ++i)
			{
				if (comps[i] is ICollidable)
				{
					(comps[i] as ICollidable).Hit(result, this);
					Destroy(gameObject);
					return;
				}
			}
		}

		// move the bullet
		transform.position += diff;
	}

    public static void CreateNewBullet(Vector3 startPos, Vector3 vel)
    {
		// handle bullet limit
		if (curBullets>=maxBullets)
			return;
		++curBullets;
		
		// create the GameObject and Bullet
		GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		Bullet bullet = obj.AddComponent<Bullet>();

		// initialize the Bullet
		bullet.transform.position = startPos;
		bullet.transform.localScale = Vector3.one*0.1f;
		bullet.vel = vel;
		bullet.collider.isTrigger = true;
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }

	public void OnDestroy() { --curBullets; }
}
