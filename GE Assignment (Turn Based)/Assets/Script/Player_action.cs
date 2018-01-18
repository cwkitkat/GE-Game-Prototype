using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_action : MonoBehaviour {

	public Turn_based_system turnSystem;
	public TurnClass001 turnClass;
	public bool yourTurn = false;

	public KeyCode moveKey1, moveKey2, aimKey1, aimKey2, shootKey, teleportKey;
	public int moveCount; //restrict player movement
	public float moveSpeed;
	public Slider stamina;
	public GameObject energy;
	public bool isKeyEnable = false;

	public GameObject pointer; //get reference for the angle to shoot
	public float pointer_turn_speed;

	public float force = 200;
	public Slider powerScale;
	public Text powerLevel;
	public GameObject powerController;

	public GameObject rocket;
	public Transform shootingPoint;

	public float countdown;
	public GameObject playerText;
	public Text warningText;

	public GameObject teleporter;

	//public bool isRight;

	void Start ()
	{
		turnSystem = GameObject.Find ("Turn-based System").GetComponent<Turn_based_system> ();

		foreach(TurnClass001 tc in turnSystem.playersGroup)
		{
			if (tc.playerGameObject.name == gameObject.name)
				turnClass = tc;
		}

		if (gameObject.tag == "Player1") 
		{
			countdown = 20; //first turn is set to be 15, 4s for players to settle down
			//isRight = true;
		}
		if (gameObject.tag == "Player2") 
		{
			countdown = 15;
			//isRight = false;
		}

		playerText.transform.Translate (-1000, 0, 0);

		moveCount = 300;
	}

	void Update () 
	{		
		yourTurn = turnClass.yourTurn;

		if (yourTurn)
		{
			//tells player's turn
			Vector3 _tempTranslation = playerText.transform.localPosition;
			float textSpeed;
			if (_tempTranslation.x < 90 && _tempTranslation.x > -50) {
				textSpeed = 100f;
			} else {
				textSpeed = 1000f;
			}
			if(_tempTranslation.x < 900)
			{
				playerText.transform.Translate (textSpeed * 2 * Time.deltaTime, 0, 0);
			}

			//show the player's weapon and stamina during his/her turn
			pointer.SetActive (true);
			powerController.SetActive (true);
			energy.SetActive(true);

			//time limit for each player's turn
			countdown -= Time.deltaTime;
			if (countdown <= 6) 
			{
				int counter = (int)countdown;
				warningText.text = counter.ToString();
				warningText.gameObject.SetActive (true);
			}
			if(countdown <= 0)
			{
				nextPlayer();
			}

			//moving left
			if (Input.GetKey (moveKey1)) 
			{
				//flipping
				//sadly i cant flip the player character, it affects the pointer rotation flickering between positive and negative rotation
				//apparently scaling doesnt work as well(the rocket shoots at the wrong direction)
				//pointer.transform.localScale =  new Vector3(-.3f, -.3f, -.3f);
				//well, stick with pointer rotation, there's one drawback: cant aim while moving
				/*int flip = -180;
				pointer.transform.rotation = Quaternion.Euler (0, flip, 0);*/
				/*if(isRight == false)
				{
					transform.Rotate(0,-180,0, Space.World);
				}*/
				//you know what, without flipping it works even better

				//disable moving right while moving left
				//reference: https://answers.unity.com/questions/320233/temporarily-disable-arrow-keys-how-.html
				if (isKeyEnable) {
					isKeyEnable = false;
				} else 
				{
					isKeyEnable = true;
				}

				if (isKeyEnable) 
				{
					//the player character moves smoothly now
					transform.position += Vector3.left * Time.deltaTime * moveSpeed;
					//transform.Translate(-1f,0f,0f); //this is another way to move
					//transform.Translate(3f*Input.GetAxis("Horizontal")*Time.deltaTime,0f,0f);
					moveCount -= 1; //lose stamina
					stamina.value = moveCount;
				}
			}

			//moving right
			if (Input.GetKey (moveKey2)) 
			{
				//flipping
				/*int flip = 0;
				pointer.transform.rotation = Quaternion.Euler (0, flip, 0);*/
				//Unity's rotation is so difficult to control
				/*if(isRight)
				{
					transform.Rotate(0,-180,0, Space.World);
				}*/

				//disable moving left while moving right
				if (isKeyEnable) {
					isKeyEnable = false;
				} else 
				{
					isKeyEnable = true;
				}

				if (isKeyEnable) 
				{
					transform.position += Vector3.right * Time.deltaTime * moveSpeed;
					moveCount -= 1;
					stamina.value = moveCount;
				}
			}

			//disable player movement
			if (moveCount <= 0) 
			{
				moveKey1 = KeyCode.None;
				moveKey2 = KeyCode.None;
				energy.SetActive (false);
			}

			//Player teleportation
			if (Input.GetKey (teleportKey)) 
			{
				force += 3;
				if (force >= 1000) {
					force = force * 0;
				}

				float training = force/1000*100;
				int supersaiyan = (int)training;
				powerScale.value = training;
				string over9000 = supersaiyan.ToString();
				powerLevel.text = over9000;
			}
			//switch to next player
			if (Input.GetKeyUp (teleportKey)) 
			{
				//disable player controls
				moveKey1 = KeyCode.None;
				moveKey2 = KeyCode.None;
				aimKey1 = KeyCode.None;
				aimKey2 = KeyCode.None;

				//set the rocket
				//Quaternion.Euler(new Vector3(0,0,transform.localEulerAngles.z))
				GameObject rocketHolder = Instantiate (teleporter, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;

				//apply wind on force
				float windRate = turnSystem.gravityRate;
				force = force + (force * windRate);

				//reference: https://l.facebook.com/l.php?u=https%3A%2F%2Fanswers.unity.com%2Fquestions%2F889787%2Fprojectiles-shoot-at-correct-angle.html&h=ATPS9BYsG2RzZFCMXBTtJ1pArEcAwO8DRbzU23DjaDf2DRsMXb4f-qq9qf6GHk_FLC8jUJciNsSJ8fMe9dHAtBNYO1O2-K0ks5UsKGELmE0fJVO1Bme99-tTY516pgUYKXqkvPaJxfxSng
				rocketHolder.GetComponent<Rigidbody> ().AddForce (shootingPoint.transform.right * force,ForceMode.Force); //shootingPoint very important

				//disable multiple shooting
				shootKey = KeyCode.None;
				teleportKey = KeyCode.None;

				//refresh conditions
				Invoke ("refreshStamina", 2.1f);

				//switch to next player's turn after shooting
				Invoke ("nextPlayer", 2.0f);
			}

			//Control Power
			if (Input.GetKey (shootKey)) 
			{
				force += 6;
				if (force >= 4000) {
					force = force * 0;
				}
				float training = force/1000*100;
				int supersaiyan = (int)training;
				powerScale.value = training;
				string over9000 = supersaiyan.ToString();
				powerLevel.text = over9000;
			}

			//switch to next player
			if (Input.GetKeyUp (shootKey)) 
			{
				//disable player controls
				moveKey1 = KeyCode.None;
				moveKey2 = KeyCode.None;
				aimKey1 = KeyCode.None;
				aimKey2 = KeyCode.None;

				//set the rocket
				//Quaternion.Euler(new Vector3(0,0,transform.localEulerAngles.z))
				GameObject rocketHolder = Instantiate (rocket, shootingPoint.transform.position, shootingPoint.transform.rotation) as GameObject;

				//apply gravity on force
				float gravityRate = turnSystem.gravityRate;
				force = force - (force * gravityRate);

				//reference: https://answers.unity.com/questions/889787/projectiles-shoot-at-correct-angle.html
				rocketHolder.GetComponent<Rigidbody> ().AddForce (shootingPoint.transform.right * force,ForceMode.Force); //shootingPoint very important

				//disable multiple shooting
				shootKey = KeyCode.None;

				//refresh conditions
				Invoke ("refreshStamina", 2.1f);

				//switch to next player's turn after shooting
				Invoke ("nextPlayer", 2.0f);
			}

			//tuning the weapon angle
			if (Input.GetKey (aimKey1))
			{
				rotateLeft ();
			}
			
			if (Input.GetKey (aimKey2))
			{
				rotateRight ();
			}
		}
	}

	void refreshStamina()
	{
		moveCount = 300;
	}

	void nextPlayer()
	{
		yourTurn = false;
		turnClass.yourTurn = yourTurn;
		turnClass.previouslyYourTurn = true;
		//reset power scale to 200
		force = 0;
		powerScale.value = force;
		powerLevel.text = "";

		//hide player's weapon when his/her turn ends
		pointer.SetActive (false);
		powerController.SetActive (false);

		/* //refresh slider rotation, decided to remove this cause it's hard for player to aim
		if (gameObject.tag == ("Player1")) 
		{
			//pointer.transform.rotation = Quaternion.Euler (0, 0, 0);
			//another method:
			pointer.transform.eulerAngles = new Vector3(0, 0, 0) ;
		}
		else if (gameObject.tag == ("Player2")) 
		{
			//pointer.transform.rotation = Quaternion.Euler (0, 180, 0);
			pointer.transform.eulerAngles = new Vector3(0, 180, 0) ;
		}*/

		//refresh player's turn text
		playerText.transform.Translate (-3200, 0, 0);
		//hide warning text
		warningText.gameObject.SetActive(false);
		//refresh countdown
		countdown = 15; 

		//refresh stamina value on slider
		stamina.value = moveCount;
		//hide stamina bar
		energy.SetActive(false);
	}

	void rotateLeft()
	{
		Vector3 temporaryRotation = pointer.transform.localEulerAngles; //stores the rotation in a vector
		temporaryRotation.z += pointer_turn_speed * Time.deltaTime;
		pointer.transform.eulerAngles = temporaryRotation; //rotates
	}

	void rotateRight()
	{
		Vector3 temporaryRotation = pointer.transform.localEulerAngles; //stores the rotation in a vector
		temporaryRotation.z -= pointer_turn_speed * Time.deltaTime;
		pointer.transform.eulerAngles = temporaryRotation; //rotates
	}
}
