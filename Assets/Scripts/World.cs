using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
    GameObject player;

	// Use this for initialization
	void Start () 
    {
        Player player = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Player>();
        player.Init();

	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
