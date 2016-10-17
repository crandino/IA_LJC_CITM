using UnityEngine;
using System.Collections;
//using BansheeGz.BGSpline.Components;

public class SteeringFollowPath : SteeringAbstract
{
    //public BGCcMath bgcc_math;
    public float radius;
    public float ahead_percentatge; // On %
    public bool reverse;
    float incr;
    Move move;
    Vector3 new_pos;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
       // incr = ahead_percentatge * bgcc_math.GetDistance() / 100.0f;       
    }

    // Update is called once per frame
    void Update ()
    {
        //new_pos = bgcc_math.CalcPositionByClosestPoint(transform.position);

        if (reverse)
            incr = -incr;       

        if (Vector3.Distance(new_pos, transform.position) < radius)
        {
            //float dist; bgcc_math.CalcPositionByClosestPoint(transform.position, out dist);
            //float ratio = (dist + incr) / bgcc_math.GetDistance();
            //ratio = ratio > 1.0f ? 0.0f : ratio;
            //new_pos = bgcc_math.CalcPositionByDistanceRatio(ratio);
        } 
        GetComponent<SteeringSeek>().Steer(new_pos);
    }

    void OnDrawGizmosSelected()
    {
        // Display the position calculated by CalcPositionByClosestPoint
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(new_pos, 0.750f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
