using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICollidable
{
    public float speed;
    private Vector3 move;
    private bool canFire;
    public float fireRate;
    Rigidbody playerRigidBody;

    public void Init()
    {
        
    }

	// Use this for initialization
	
    void Awake()
    {
        this.transform.position = new Vector3(0, 0, 0);
        playerRigidBody = GetComponent<Rigidbody>();
        canFire = true;
        fireRate = 0.33f;
        speed = 0.07f;
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
        playerRigidBody.MovePosition(transform.position + move * speed);

        // Check if the player can fire a bullet
        if (canFire)
        {
            // When the button to fire bullets is pushed
            if (Input.GetButtonDown("Fire1"))
            {
                // Get the vector from where the mouse was clicked
                Vector3 mouseV = Input.mousePosition;
                // Get the vector based on the screen position of the rigidBody to the mouse position clicked
                Vector3 vel = mouseV - Camera.main.WorldToScreenPoint(transform.position);
                vel.Normalize();

                // Find the rigidBody padding of where to spawn a bullet away from character
                Vector3 spawnPos = transform.position + vel * (this.GetComponent<Renderer>().bounds.size.x * 0.3f) + vel * (this.GetComponent<Renderer>().bounds.size.y * 0.3f);
                canFire = false;
                Bullet.CreateNewBullet(spawnPos, vel * 4);
                StartCoroutine(RefreshFire());
            }
            else
            {
                Vector3 aim = new Vector3(rightX, rightY, 0.0f);
                aim.Normalize();
                if (aim != Vector3.zero)
                {
                    Vector3 spawnPos = transform.position + aim * (this.GetComponent<Renderer>().bounds.size.x * 0.3f) + aim * (this.GetComponent<Renderer>().bounds.size.y * 0.3f);
                    canFire = false;
                    Bullet.CreateNewBullet(spawnPos, aim * 4);
                    StartCoroutine(RefreshFire());
                }
            }
        }
    }

    public IEnumerator RefreshFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    public void Hit(RaycastHit rayhit, Bullet bullet)
    {

    }
}
