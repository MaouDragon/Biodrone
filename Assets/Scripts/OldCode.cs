using UnityEngine;
using System.Collections;

public class OldCode : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		/**
		// Check if the player can fire a bullet
		if (canFire)
		{
			// When the button to fire bullets is pushed
			if (Input.GetButtonDown("Fire1"))
			{
				// Get the vector from where the mouse was clicked
				Vector3 mouseV = Input.mousePosition;
				// Get the vector based on the screen position of the rigidBody to the mouse position clicked
				Vector3 aim = mouseV - Camera.main.WorldToScreenPoint(transform.position);
				
				// Find the rigidBody padding of where to spawn a bullet away from character
				canFire = false;
				FireBullet(aim);
				StartCoroutine(RefreshFire());
			}
			else
			{
				Vector3 aim = new Vector3(rightX, rightY, 0.0f);
				if (aim != Vector3.zero)
				{
					canFire = false;
					FireBullet(aim);
					StartCoroutine(RefreshFire());
				}
			}
		}

		shieldCounter -= Time.fixedDeltaTime;
		if (shieldCounter<0 && Input.GetButtonDown("Fire2"))
			shieldCounter = 4;
		shield.SetActive(shieldCounter>1);

	private void FireBullet(Vector3 dir)
    {
        dir.Normalize();
        Vector3 spawnPos = transform.position + dir * (this.GetComponent<Renderer>().bounds.size.x * 0.3f) + dir * (this.GetComponent<Renderer>().bounds.size.y * 0.3f);
        Bullet.CreateNewBullet(spawnPos, dir * 12, typeof(PlayerBullet));
        GetComponentInChildren<PlayerCamera>().Shake(dir);
    }

    public IEnumerator RefreshFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
