using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportPlayer2 : MonoBehaviour {

	public float countdown = 7;
	public GameObject effect, circleEffect, player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		//destroy the projectile after 7 seconds in game
		countdown -= Time.deltaTime;
		if (countdown <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		player.transform.localPosition = gameObject.transform.localPosition;
		circleEffect = Instantiate (effect, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject); //destroy the projectile upon hitting an object
	}
}
