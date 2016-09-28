using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour {

	public float min_distance = 0.1f;
	public float slow_distance = 5.0f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () { 
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
		if(!move)
			move = GetComponent<Move>();

        // TODO 3: Create a vector to calculate our ideal velocity
        // then calculate the acceleration needed to match that velocity
        // before sending it to move.AccelerateMovement() clamp it to 
        // move.max_mov_acceleration

        float dist = Vector3.Distance(target, transform.position);

        if (dist < min_distance)
            move.SetMovementVelocity(Vector3.zero);
        else if(dist < slow_distance)
        {
            Vector3 diff = (target - transform.position).normalized * move.max_mov_velocity;
            diff = Vector3.Lerp(diff, Vector3.zero, 1 - (dist / slow_distance)) / time_to_target;
            Vector3 acc = (diff - move.movement);

            if (acc.magnitude > move.max_mov_acceleration)
                acc = Vector3.ClampMagnitude(acc, move.max_mov_acceleration);
            move.AccelerateMovement(acc);
        }  
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}
