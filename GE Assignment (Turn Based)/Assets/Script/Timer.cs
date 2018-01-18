using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float countdown;
	public Text gameStart, clock;
	public GameObject timesUp;

	// Use this for initialization
	void Start () 
	{
		countdown = 60*3;	//five minutes game time
		//reference for activating UI component: https://answers.unity.com/questions/1151136/sliderenabled-false.html
		gameStart.gameObject.SetActive(true);
		Invoke("begin",3f);
	}

	void begin()
	{
		Destroy(gameStart);
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		//reference: https://forum.unity.com/threads/converting-float-to-integer.27511/
		//reference: https://docs.unity3d.com/ScriptReference/String.html
		int counter = (int)countdown;
		string timeRemains = counter.ToString ();
		clock.text = "Time left: " + timeRemains;
		if(countdown <= 0)
		{
			//show time's up text
			timesUp.SetActive(true);
		}
	}
}
