using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterLanding : MonoBehaviour {

	//reference: https://www.youtube.com/watch?v=7KiK0Aqtmzc&index=1&list=PLoOC5bkXs3U0Zq0ivsaMWXixWX4RDOX3T
	public float fallMultiplier = 3.5f;
	Rigidbody rB;

	// Use this for initialization
	void Start () {
		rB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rB.velocity.y < 10) 
		{
			rB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
	}
}
