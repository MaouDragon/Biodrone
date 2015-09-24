using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallManager : MonoBehaviour, ICollidable {

	public List<Wall> walls;
	public int curIndex;
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Hit(RaycastHit rayhit, Bullet bullet) {
		walls[curIndex].Hit(rayhit, bullet);
		curIndex = (curIndex+1)%walls.Count;
    }
}
