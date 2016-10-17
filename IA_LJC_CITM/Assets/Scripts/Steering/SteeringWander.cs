using UnityEngine;
using System.Collections;

public class SteeringWander : SteeringAbstract
{
    public Vector3 off_set;
    public float radius;
    Move move;
    SteeringSeek steering_seek;
    Vector3 new_pos;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        steering_seek = GetComponent<SteeringSeek>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position);
    }

    public void Steer(Vector3 target)
    {
        if (!move)
            move = GetComponent<Move>();

        Vector3 center_circle = transform.position + off_set;
        new_pos = new Vector3(Random.insideUnitCircle.x, 0.0f, Random.insideUnitCircle.y) * radius + center_circle;

        steering_seek.Steer(new_pos);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(off_set), radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new_pos, 0.5f);
    }
}

