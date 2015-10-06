using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICollidable
{
    public float speed;
    private Vector3 move;
    Rigidbody playerRigidBody;
    private SpriteRenderer sprite;
	private GameObject shield;

	public float shieldCounter;
	public int health=10;

    public void Init()
    {
        
    }

    // Use this for initialization

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        shield = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        
	}

    void FixedUpdate()
    {
		transform.position = new Vector3(transform.position.x, transform.position.y);
		KeyPress();
        
		//shield.transform.position = transform.position;
    }

    // Calculate the vectors based on the input given
    public void KeyPress()
    {
        // Get the horizontal and vertical movement
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");
        // Get horizontal and vertical aim from the right joystick
        float rightX = Input.GetAxis("RightX");
        float rightY = Input.GetAxis("RightY");

        // Set and normalize the move vector
        move.Set(translationX, translationY, 0);
        move.Normalize();
        // Translate the rigidBody to the input movement
		playerRigidBody.velocity = move * speed / Time.fixedDeltaTime / 2;
        playerRigidBody.MovePosition(transform.position + move * speed / 2);
		//Bullet.MoveCollidables(new GameObject[] { gameObject, shield }, move * speed);

        if (move != Vector3.zero)
            GetComponentInChildren<SpriteRenderer>().transform.up = playerRigidBody.velocity;

        // Get the vector from where the mouse was clicked
        Vector3 mouseV = Input.mousePosition;
		// Get the vector based on the screen position of the rigidBody to the mouse position clicked
		//Vector3 aim = mouseV - Camera.main.WorldToScreenPoint(transform.position);

        // handle the shield
        //shield.transform.LookAt(transform.position - new Vector3(aim.x, aim.y, 0.0f), Vector3.back);
        if ((rightX != 0) || (rightY != 0)) 
            shield.transform.LookAt(transform.position - new Vector3(rightX, rightY, 0), Vector3.back);
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {
        if (rayhit.collider.gameObject.Equals(shield))
        {
            shield.GetComponent<PlayerWall>().Hit(rayhit, bullet);
        }
        else
        {
            
            GetComponentInChildren<PlayerCamera>().Shake(bullet.vel.magnitude);
            if (--health <= 0)
                Application.LoadLevel(0);
        }
    }
}
