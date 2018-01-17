using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour {

	//reference: https://www.youtube.com/watch?v=fqCW64Dku1Y
	public static int P1score, P2score;
	public Text text1, text2;

	public GameObject clock, pause;
	Timer timer;

	public int index1, index2, index3, index4;

	void Awake()
	{
		text1 = text1.GetComponent <Text> ();
		text2 = text2.GetComponent <Text> ();
		P1score = 0;
		P2score = 0;
		timer = clock.GetComponent<Timer> ();
	}

	void Start()
	{
		text1.gameObject.SetActive (true); 
		text2.gameObject.SetActive(true);
	}

	void Update () 
	{
		text1.text = "Score:" + P1score;
		text2.text = "Score:" + P2score;

		//quit game instantly
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			SceneManager.LoadScene (index1);
		}

		//pause game
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			if (Time.timeScale == 1) 
			{
				Time.timeScale = 0;
				pause.SetActive (true);
			}
			else
			{
				Time.timeScale = 1;
				pause.SetActive (false);
			}
		}

		if (timer.countdown <= 0) 
		{
			if (P1score > P2score) 
			{
				Invoke("P1_win",3f);
			} 
			else if (P2score > P1score) 
			{
				Invoke("P2_win",3f);
			} 
			else if (P1score == P2score) 
			{
				Invoke("tie",3f);
			}
		}
	}
		
	void P1_win()
	{SceneManager.LoadScene (index2);}
	void P2_win()
	{SceneManager.LoadScene (index3);}
	void tie()
	{SceneManager.LoadScene (index4);}
}
