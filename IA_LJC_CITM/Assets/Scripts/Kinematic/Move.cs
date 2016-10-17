using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

    Vector3[] movement_priorities;
	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;
	public float max_mov_acceleration = 0.1f;
    public float max_rot_velocity; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

	[Header("-------- Read Only --------")]
	public Vector3 movement = Vector3.zero;
	public float rotation = 0.0f; // degrees

    void Start()
    {
        movement_priorities = new Vector3[SteeringConf.num_priorities];
    }

    // Update is called once per frame
    void Update()
    {
        //Selecting movement
        //for (uint i = 0; i < SteeringConf.num_priorities; ++i)
        //{
        //    if (movement_priorities[i].magnitude > 0.0f)
        //    {
        //        movement += movement_priorities[i];
        //        break;
        //    }
        //}

        // cap velocity
        if (movement.magnitude > max_mov_velocity)
        {
            movement.Normalize();
            movement *= max_mov_velocity;
        }

        // cap rotation
        rotation = Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

        // rotate the arrow
        float angle = Mathf.Atan2(movement.x, movement.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        // strech it
        arrow.value = movement.magnitude * 4;

        // final rotate
        transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

        // finally move
        transform.position += movement * Time.deltaTime;

        // Resetting movement_priorities
        for(uint i = 0; i < SteeringConf.num_priorities; ++i)
            movement_priorities[i] = Vector3.zero;
    }

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity (Vector3 velocity) 
	{
        movement = velocity;
	}

	public void AccelerateMovement (Vector3 velocity, int priority) 
	{
        movement_priorities[priority] += velocity;
	}

	public void SetRotationVelocity (float rotation_velocity) 
	{
		rotation = rotation_velocity;
	}

	public void AccelerateRotation (float rotation_acceleration) 
	{
		rotation += rotation_acceleration;
	}
}
