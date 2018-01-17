using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProjectile : MonoBehaviour {

	public int score = 1;
	public float countdown = 7;
	public GameObject effect, dust;

	void Update()
	{
		//destroy the projectile after 7 seconds in game
		countdown -= Time.deltaTime;
		if (countdown <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "Player1") 
		{
			Scoring.P2score += score;
		}

		if (other.gameObject.tag == "Player2") 
		{
			Scoring.P1score += score;
		}

		//reference: https://www.youtube.com/watch?v=Sny523MrTd0
		//explosive effect
		dust = Instantiate (effect, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject); //destroy the projectile upon hitting an object
	}
}
