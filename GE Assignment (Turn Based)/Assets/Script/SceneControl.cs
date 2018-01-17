using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void SceneChange () {
		SceneManager.LoadScene (0);
	}

	public void PlayButton ()
	{
		SceneManager.LoadScene (1);
	}
}
