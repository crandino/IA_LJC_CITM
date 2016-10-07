using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RayCastSpecs
{
    public Vector3 off_set;
}

public class SteeringObstacleAvoidance : SteeringAbstract
{

    public RayCastSpecs[] ray_casts;
	public LayerMask mask;
    public float avoid_distance;

	Move move;
	SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>(); 
		seek = GetComponent<SteeringSeek>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Agents must avoid any collider in their way
        // 1- Create your own (serializable) class for rays and make a public array with it
        // 2- Calculate a quaternion with rotation based on movement vector
        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info

        RaycastHit ray_hit;        

        for (int i = 0; i < ray_casts.Length; ++i)
        {
            Ray ray = new Ray(transform.position + ray_casts[i].off_set, move.movement);
            if (Physics.Raycast(ray, out ray_hit, avoid_distance, mask))
            {
                Vector3 acc = move.movement + Vector3.ProjectOnPlane(ray_hit.normal, Vector3.up);
                move.AccelerateMovement(acc);
            }                
        }

		// 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)
	}

	void OnDrawGizmosSelected() 
	{
		if(move && this.isActiveAndEnabled)
		{
			Gizmos.color = Color.red;
			float angle = Mathf.Atan2(move.movement.x, move.movement.z);
			Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)
            for (int i = 0; i < ray_casts.Length; ++i)
            {
                Vector3 origin = transform.position + q * ray_casts[i].off_set;
                Gizmos.DrawLine(origin, origin + move.movement.normalized * avoid_distance);
            }
        }
	}
}
