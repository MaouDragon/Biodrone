using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICollidable
{
    public float speed;
    private Vector3 move;
    Rigidbody playerRigidBody;
    public void Init()
    {
        
    }

	// Use this for initialization
	
    void Awake()
    {
        this.transform.position = new Vector3(0, 0, 0);
        playerRigidBody = GetComponent<Rigidbody>();
        speed = 0.12f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void FixedUpdate()
    {
        KeyPress();
    }

    // Calculate the vectors based on the input given
    public void KeyPress ()
    {
        // Get the horizontal and vertical movement
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");
        // Set and normalize the move vector
        move.Set(translationX, translationY, 0);
        move.Normalize();
        // Translate the rigidBody to the input movement
        playerRigidBody.MovePosition(transform.position + move * speed);

        // When the button to fire bullets is pushed
        if (Input.GetButtonDown("Fire1"))
        {
            // Get the vector from where the mouse was clicked
            Vector3 mouseV = Input.mousePosition;
            // Get the vector based on the screen position of the rigidBody to the mouse position clicked
            Vector3 vel = mouseV-Camera.main.WorldToScreenPoint(transform.position);
            vel.Normalize();
            
            // Find the rigidBody padding of where to spawn a bullet away from character
            Vector3 spawnPos = transform.position + vel*(int)(this.renderer.bounds.size.x*0.55f);
            Bullet.CreateNewBullet(spawnPos, vel*2);
        }
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }
}
