﻿using UnityEngine;
using System.Collections;

public class SteeringSeek : SteeringAbstract
{

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
        if (!move)
            move = GetComponent<Move>();

        // TODO 1: accelerate towards our target at max_acceleration
        // use move.AccelerateMovement()
        Vector3 diff = (target - transform.position).normalized;
        diff *= move.max_mov_acceleration;
        move.AccelerateMovement(diff);

	}
}
