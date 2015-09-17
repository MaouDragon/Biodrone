using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour, ICollidable
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public virtual void Hit(RaycastHit rayhit, Bullet bullet)
    {
		// create a new bullet
		Bullet.CreateNewBullet(
			ReflectedStartPos(rayhit, bullet),
			ReflectedVelocity(rayhit, bullet));
    }

	protected virtual Vector3 ReflectedStartPos(RaycastHit rayHit, Bullet bullet, Vector3 vel=default(Vector3))
	{
		if (vel==default(Vector3))
			vel = ReflectedVelocity(rayHit, bullet);
		float totalDist = bullet.vel.magnitude*Time.fixedDeltaTime;
		return rayHit.point+(totalDist-rayHit.distance)*vel;
	}

	protected virtual Vector3 ReflectedNormal(RaycastHit rayHit, Bullet bullet)
	{
		// collect variables
		Mesh m = gameObject.GetComponent<MeshFilter>().mesh;
		Vector3[] verts = m.vertices;
		int[] tris = m.triangles;
		int index = 3*rayHit.triangleIndex;

		// create the plane to find the normal
		Plane plane = new Plane(
			transform.TransformPoint(verts[tris[index+0]]),
			transform.TransformPoint(verts[tris[index+1]]),
			transform.TransformPoint(verts[tris[index+2]]));
		return plane.normal;
	}

	protected virtual Vector3 ReflectedVelocity(RaycastHit rayHit, Bullet bullet)
	{
		// perform the reflection
		Vector3 normal = ReflectedNormal(rayHit, bullet);
		normal = new Vector3(normal.x, normal.y, 0).normalized;
		Vector3 newDir = Vector3.Reflect(bullet.vel, normal);
		return newDir.normalized*bullet.vel.magnitude*1f;
	}
}
