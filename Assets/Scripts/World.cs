using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
    private GameObject player;
    public GameObject wall;
    private bool trigger1 = true;

	// Use this for initialization
	void Start () 
    {
        // Initialize the Bullet Start() function to prevent an KeyNotFoundException when firing a bullet
        Bullet bullet = new Bullet();
        bullet.Start();
        player = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (trigger1 && player.transform.position.y >= 18)
        {
            Instantiate(wall, new Vector3(0.0f, 16.78f, 0.0f), Quaternion.identity);
            trigger1 = false;
        }
	}
}
