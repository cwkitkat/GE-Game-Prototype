using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn_based_system : MonoBehaviour {

	//reference:https://www.youtube.com/watch?v=rMuana_NQnU
	public List<TurnClass001> playersGroup;
	public Player_action pa1, pa2;

	public float windRate;
	public Text windPower;
	public double windFriction;

	void Start () 
	{
		ResetTurns ();
	}

	void Update () 
	{
		UpdateTurns ();
	}

	void ResetTurns()
	{
		for (int i = 0; i < playersGroup.Count; i++) 
		{
			if (i == 0) {
				playersGroup [i].yourTurn = true;
				playersGroup [i].previouslyYourTurn = false;
				//reset locked key to become functional again for both players
				pa1.moveKey1 = KeyCode.LeftArrow;
				pa1.moveKey2 = KeyCode.RightArrow;
				pa2.moveKey1 = KeyCode.LeftArrow;
				pa2.moveKey2 = KeyCode.RightArrow;
				pa1.aimKey1 = KeyCode.UpArrow;
				pa1.aimKey2 = KeyCode.DownArrow;
				pa2.aimKey1 = KeyCode.UpArrow;
				pa2.aimKey2 = KeyCode.DownArrow;
				pa1.shootKey = KeyCode.Space;
				pa2.shootKey = KeyCode.Space;
				//generating random wind force
				wind ();
			}
			else 
			{
				playersGroup [i].yourTurn = false;
				playersGroup [i].previouslyYourTurn = false;

			}
		}
	}

	void UpdateTurns()
	{
		for (int i = 0; i < playersGroup.Count; i++) 
		{
			if (!playersGroup [i].previouslyYourTurn) 
			{
				playersGroup [i].yourTurn = true;
				break;
			} 
			else if (i == playersGroup.Count - 1 && playersGroup [i].previouslyYourTurn) 
			{
				ResetTurns ();
			}
		}
	}

	void wind ()
	{
		//randomize wind force, reference: https://docs.unity3d.com/ScriptReference/Random.Range.html
		float a = -0.2f;
		float b = 0.2f;
		windRate = Random.Range(a, b);
		windFriction = (double)windRate;
		//reference: https://forum.unity.com/threads/how-to-round-a-float-to-2-decimal-places.361504/
		windPower.text = "wind: " + windFriction.ToString("0.00");
	}
}


[System.Serializable]
public class TurnClass001
{
	public GameObject playerGameObject;
	public bool yourTurn = false;
	public bool previouslyYourTurn = false;
}