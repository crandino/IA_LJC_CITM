using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

    public float min_angle;
    public float slow_angle;
    public float time_to_target;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
     }

	// Update is called once per frame
	void Update () 
	{
        // TODO 4: As with arrive, we first construct our ideal rotation
        // then accelerate to it. Use Mathf.DeltaAngle() to wrap around PI
        // Is the same as arrive but with angular velocities

        float target_angle = Mathf.Atan2(move.target.transform.forward.x, move.target.transform.forward.z) * Mathf.Rad2Deg;
        float tank_angle = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float delta = Mathf.DeltaAngle(tank_angle, target_angle);
        float abs_delta = Mathf.Abs(delta);

        if (abs_delta < min_angle)              // Reaching minimum angle -> STOP
            move.SetRotationVelocity(0.0f);
        else if (abs_delta < slow_angle)        // Entering slow_angle -> SLOW DOWN
        {
            float rot_vel;
            if (delta > 0)  //clockwise
                rot_vel = Mathf.LerpAngle(move.max_rot_velocity, 0.0f, 1.0f - (abs_delta / slow_angle));
            else            // Counter clockwise
                rot_vel = Mathf.LerpAngle(-move.max_rot_velocity, 0.0f, 1.0f - (abs_delta / slow_angle));
                        
            move.AccelerateRotation(rot_vel - move.rotation);            
        }
        else                                    // Otherwise -> SPEED UP TO FULL SPEED
        {
            float rot_vel = delta;
            rot_vel = Mathf.Clamp(rot_vel, -move.max_rot_velocity, move.max_rot_velocity) / time_to_target;
            float rot_acc = rot_vel - move.rotation;
            rot_acc = Mathf.Clamp(rot_acc, -move.max_rot_acceleration, move.max_rot_acceleration);
            move.AccelerateRotation(rot_acc);
        }
    }
}
