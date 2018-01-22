using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideFadeImg : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("hideYourself",2f);
	}

	void hideYourself()
	{
		gameObject.SetActive (false);
	}
}
