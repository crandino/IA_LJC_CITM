using UnityEngine;
using System.Collections;

public class AnimControl : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim_controller;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim_controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < agent.radius)
            anim_controller.SetBool("run", false);
        else
        {
            anim_controller.SetBool("run", true);
            anim_controller.SetFloat("vel_x", agent.velocity.x);
            anim_controller.SetFloat("vel_y", agent.velocity.z);
        }            
    }
}
