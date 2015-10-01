using UnityEngine;
using System.Collections;

public class Turret:Enemy {
	public override void Update() {
		// rotate towards the player
		transform.LookAt(player.transform.position, Vector3.back);
		
		base.Update();
	}
}