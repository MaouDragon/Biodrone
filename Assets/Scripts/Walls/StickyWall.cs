using UnityEngine;
using System.Collections;

public class StickyWall : Wall
{
	public override void Hit(RaycastHit rayhit, Bullet bullet) {
		TryBreak(rayhit, bullet);
	}
}
