using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SteeringObstacleAvoidance))]
public class SteeringObstacleAvoidanceEditor : Editor
{
    private SteeringObstacleAvoidance steer_inspector;
    private Move move;

    void OnEnable()
    {
        steer_inspector = target as SteeringObstacleAvoidance;
        move = steer_inspector.GetComponent<Move>();
    }

    [ExecuteInEditMode]
    void OnSceneGUI()
    {
        if(!Application.isPlaying)
        {
            Handles.color = Color.red;    

            for (int i = 0; i < steer_inspector.ray_casts.Length; ++i)
            {
                Vector3 origin = steer_inspector.transform.position + steer_inspector.ray_casts[i].off_set;
                Handles.DrawLine(origin, origin + steer_inspector.transform.forward * steer_inspector.avoid_distance);
            }
        }
       
    }

}