﻿using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, 3.5f * Time.deltaTime);
	}
}
