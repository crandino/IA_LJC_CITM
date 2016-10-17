using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveToMouseClick : MonoBehaviour {

    NavMeshAgent agent;
    // Mejor array de GameObjects[] agents;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out hit) == true)
            {
                //transform.position = hit.point;
                agent.SetDestination(hit.point);
            }				
		}
	}

}
