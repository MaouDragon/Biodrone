using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
    GameObject player;

	// Use this for initialization
	void Start () 
    {
        // Initialize the Bullet Start() function to prevent an KeyNotFoundException when firing a bullet
        Bullet bullet = new Bullet();
        bullet.Start();
        //Player player = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Player>();
        //player.Init();

	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
