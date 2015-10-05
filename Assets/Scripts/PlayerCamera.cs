using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;
    Camera cam;
    private float shakeDist;
    private float shakeDur = 0;
    private Vector3 distance;
    
	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
        player = cam.transform.parent.gameObject;
        distance = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (shakeDist > 0)
        {
            transform.position = new Vector3(player.transform.position.x + Random.Range(-shakeDist, shakeDist), player.transform.position.y + Random.Range(-shakeDist, shakeDist), transform.position.z);
            shakeDist -= shakeDur * Time.deltaTime;
        }
        else
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}

    public void Shake(float dist, float duration=0.75f)
    {
        // Reset camera position
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        shakeDist = dist/30;
        shakeDur = duration;
    }
}
