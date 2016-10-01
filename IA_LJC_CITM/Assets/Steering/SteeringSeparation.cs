using UnityEngine;
using System.Collections;

public class SteeringSeparation : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float relative_distance;
    public float search_radius = 5.0f;
    public LayerMask mask;	
	public AnimationCurve falloff;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Agents much separate from each other:
        // 1- Find other agents in the vicinity (use a layer for all agents)
        // 2- For each of them calculate a escape vector using the AnimationCurve
        // 3- Sum up all vectors and trim down to maximum acceleration

        Collider[] colliders = Physics.OverlapSphere(transform.position, search_radius, mask);
        Vector3 escape_dir = Vector3.zero;

        for(int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i] == GetComponent<BoxCollider>())
                continue;

            Vector3 temp = (transform.position - new Vector3(colliders[i].transform.position.x, 0.0f, colliders[i].transform.position.z));

            relative_distance = 1.0f - Vector3.Distance(colliders[i].transform.position, transform.position) / search_radius;
            temp *= falloff.Evaluate(relative_distance);                        

            escape_dir += temp;          
        }

        move.AccelerateMovement(escape_dir);
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
