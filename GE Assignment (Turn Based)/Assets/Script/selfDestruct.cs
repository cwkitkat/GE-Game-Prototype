using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("selfDestruction", 3f);
	}
	
	void selfDestruction()
	{
		Destroy(gameObject);
	}
}
