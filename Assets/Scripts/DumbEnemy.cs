using UnityEngine;
using System.Collections;

public class DumbEnemy:Enemy {
	public float speed;

	public override void Update() {
		// move towards the player
		Vector3 dir = (player.transform.position-transform.position).normalized;
		GetComponent<Rigidbody>().velocity = dir*speed;
		transform.position = new Vector3(transform.position.x, transform.position.y);

		base.Update();
	}
}