using UnityEngine;
using System.Collections;

public class KinematicFlee : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 6: To create flee just switch the direction to go
        Vector3 direction = (move.target.transform.position - transform.position);
        direction.x *= -1;
        direction.z *= -1;
        move.SetMovementVelocity(direction);
        move.mov_velocity = move.mov_velocity.normalized * move.max_mov_velocity;
    }
}
