using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICollidable
{
    public void Init()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void KeyPress ()
    {
        //if (Input.GetKeyDown(Keyboard.LeftArrow))
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }
}
