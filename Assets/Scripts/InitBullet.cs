using UnityEngine;
using System.Collections;

public class InitBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Bullet.CreateNewBullet(transform.position, transform.TransformVector(Vector3.up));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
