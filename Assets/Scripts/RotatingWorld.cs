﻿using UnityEngine;
using System.Collections;

public class RotatingWorld : MonoBehaviour
{
    public Camera cam;
    private GameObject player;
    public GameObject wall;
    public GameObject doorFloor;
    public GameObject[] firstSwitches;
    public GameObject[] secondSwitches;
    public GameObject firstWall;
    public GameObject floorSprite;
    public GameObject playerBullet;
    public GameObject normalBullet;
    public Material bulletTrail;

    // Use this for initialization
    void Start()
    {
        player = transform.parent.gameObject;
        Bullet.playerBullet = playerBullet;
        Bullet.normalBullet = normalBullet;
        Bullet.bulletTrail = bulletTrail;

        doorFloor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((firstSwitches[0].GetComponent<SwitchWall>().switchOn) && (firstSwitches[1].GetComponent<SwitchWall>().switchOn) &&
            (firstSwitches[2].GetComponent<SwitchWall>().switchOn) && (firstSwitches[3].GetComponent<SwitchWall>().switchOn))
        {
            Destroy(firstWall);
            doorFloor.SetActive(true);
        }
        cam.transform.Rotate(Vector3.forward, -5*Time.deltaTime);
        floorSprite.transform.Rotate(Vector3.forward, -5 * Time.deltaTime);
    }
}
