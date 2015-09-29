using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;
    Camera cam;
    public float panSpeed; // 1.0 = 1 second on a one-way pan, 2.0 = 0.5 second
    private float range;
    private Vector3 distance;
    
	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
        player = cam.transform.parent.gameObject;
        distance = Vector3.zero;
        panSpeed = 30.0f;
        range = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //cam.transform.Translate(speed, speed, 0.0f);
        cam.transform.position = new Vector3(player.transform.position.x + distance.x * range, player.transform.position.y + distance.y * range, transform.position.z);
        //Debug.Log(player.transform.position.x);
        Pan();
	}

    public void Shake(Vector3 direction)
    {
        // Invert then normalize
        direction = -direction;
        direction.Normalize();
        distance = direction * 0.05f;
        // Start the range close to the player to start incrementing the range
        range = 0.01f;
        panSpeed = Mathf.Abs(panSpeed);
    }

    private void Pan()
    {
        // Only pan if range isn't stopped at 0
        if (range > 0.0f)
        {
            // Slowly increment the range up to 100%
            if (range < 1.0f)
                range += panSpeed * Time.deltaTime;
            else // Then decrement back to 0%
            {
                panSpeed = -panSpeed;
                range = 0.99f;
            }
        }
        else // Stop panning when finished
        {
            range = 0.0f;
            panSpeed = Mathf.Abs(panSpeed);
        }
    }
}
