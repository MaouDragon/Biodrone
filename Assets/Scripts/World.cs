﻿using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
    private GameObject player;
    public GameObject wall;
    private bool trigger1 = true;
	public GameObject[] firstSwitches;
	public GameObject[] secondSwitches;
	public GameObject firstWall;
	public GameObject playerBullet;
	public GameObject normalBullet;
	public Material bulletTrail;

	// Use this for initialization
	void Start () 
    {
        player = transform.parent.gameObject;
		Bullet.playerBullet = playerBullet;
		Bullet.normalBullet = normalBullet;
		Bullet.bulletTrail = bulletTrail;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if ((firstSwitches[0]==null) && (firstSwitches[1]==null) &&
		    (firstSwitches[2]==null) && (firstSwitches[3]==null))
			Destroy(firstWall);
	}
}
