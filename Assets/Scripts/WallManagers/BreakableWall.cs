using UnityEngine;
using System.Collections;

public class BreakableWall : Wall
{
    GameObject wall;
    int hitCount = 0;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public override void Hit(RaycastHit rayHit, Bullet bullet)
    {
        hitCount++;
        Debug.Log(hitCount);
        if (hitCount >= 20)
            Destroy(wall);
    }
}
