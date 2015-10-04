using UnityEngine;
using System.Collections;

public class Turret:Enemy {
	public override void Update() {
		// rotate towards the player
		transform.LookAt(player.transform.position, Vector3.back);
		
		base.Update();
	}

	public override void Fire ()
	{
		if ((player.transform.position - transform.position).magnitude < (new Vector3(20.0f, 20.0f, 0.0f)).magnitude) {
			base.Fire();
		}
	}
}