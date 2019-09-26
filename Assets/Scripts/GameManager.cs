using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages game information such as levels, enabling entities, and checking the game status
 */
public class GameManager : MonoBehaviour {

	//References to other GameObjects and scripts
	PlanetRotate planetRotate;
	ShipMove shipMove;
	ManagePlatforms createPlatforms;
	PlanetFall planetFall;
	OpenMenu openMenu;

	//Location and rotation of the ship
	Vector3 shipLocation;
	Quaternion shipRotation;

	//Number of platforms that still exist
	public int remainingPlatforms;

	//Current level
	public int Level;

	//Game information that changes with each level
	private int platforms;
	public int Platforms {
		get{ return platforms; }
		set{ platforms = value; }
	}
	private float planetSpeed;
	public float PlanetSpeed {
		get{ return planetSpeed; }
		set{ planetSpeed = value; }
	}
	private float shipSpeed;
	public float ShipSpeed {
		get{ return shipSpeed; }
		set{ shipSpeed = value; }
	}

	//This instance
	GameManager ThisGameManager;

	//Disable VSync
	void Awake(){
		QualitySettings.vSyncCount = 0;
	}

	// Use this for initialization
	void Start () {
		ThisGameManager = this;
		planetRotate = GameObject.FindGameObjectWithTag ("Planet").GetComponent<PlanetRotate> ();
		planetFall = GameObject.FindGameObjectWithTag ("Planet").GetComponent<PlanetFall> ();
		shipMove = GameObject.FindGameObjectWithTag ("Player").GetComponent<ShipMove> ();
		openMenu = GameObject.FindGameObjectWithTag ("Settings").GetComponent<OpenMenu> ();
		createPlatforms = GameObject.FindGameObjectWithTag ("Planet").GetComponent<ManagePlatforms> ();

		//Disable the ship when the game first starts
		shipMove.gameObject.SetActive (false);

		//Change speed and rotation for title screen
		Level = 0;
		PlanetSpeed = 5f;
		ShipSpeed = 0f;
		Platforms = 0;
		//TODO: CREATE AN ANGLE VARIABLE FOR MORE THAN 15 PLATFORMS
		//TODO: IN FIXEDUPDATE LOOPS OF PLANET AND SHIP- RETURN IF THESE ARE 0 TO SAVE RESOURCES

	}

	/*
	 * Check if the user started the game and if there are any remaining platforms
	 */
	void Update(){
		if (Level <= 0) {
			//When user taps the planet at level 0, advaceLevel();
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null && hit.transform.gameObject.tag == "Planet" && planetFall.gameObject.transform.position == Vector3.zero) {
				//TODO: PLANET WILL GRADUALLY SPEED UP ON GAME START
				advanceLevel ();
				shipMove.gameObject.SetActive (true);
				//TODO: HIDE LEVEL SELECT AND SETTINGS

			}
		}

		//Check for remaining platforms
		if (remainingPlatforms == 0 && Level > 0) {
			advanceLevel ();
		}

	
	}

	/*
	 * Increment the level number and assign values accordingly
	 */
	public void advanceLevel(){
		Level++;
		//Change speed and rotation based on level number
		if(Level == 1){ 
			PlanetSpeed = 15f; 
			ShipSpeed = 50f;
			Platforms = 4;
		}
		if (Level == 2) {
			PlanetSpeed = 30f;
			ShipSpeed = 50f;
			Platforms = 5;
		}
		if (Level == 3) {
			PlanetSpeed = 40f;
			ShipSpeed = 50f;
			Platforms = 5;
		}
		if (Level == 4) {
			PlanetSpeed = 40f;
			ShipSpeed = 55f;
			Platforms = 5;
		}
		if (Level == 5) {
			PlanetSpeed = 40f;
			ShipSpeed = 55f;
			Platforms = 6;
		}
		if (Level == 6) {
			PlanetSpeed = 60f;
			ShipSpeed = 60f;
			Platforms = 6;
		}

		//Reset platform counter
		remainingPlatforms = Platforms;

		//Create the platforms
		createPlatforms.InitPlatforms ();

	}

	/*
	 * For when the planet is hit
	 */
	public void GameOver(){

		//Save ship data
		shipMove.Disable ();
		shipLocation = shipMove.transform.position;
		shipRotation = shipMove.transform.rotation;

		//Remove remaining platforms
		createPlatforms.destroyPlatforms ();

		//Make the Planet "fall"
		planetFall.Fall ();
		//Debug.Log ("FALL");

		//---This point on restarts the game---//

		//Grow the Planet back into existence//This method in turn calls PlanetFall.growPlanet after 1.5 seconds
		planetFall.Grow ();

	}

	/*
	 * Called from PlanetFall.FixedUpdate()//Recreates platforms and ship after death
	 */
	public void reenableStuff(){
		//Reinstantiate remaining platforms
		createPlatforms.InitPlatforms();

		//Reset platform counter
		remainingPlatforms = Platforms;

		//Reset Ship
		//TODO: Ship will be launched in from off the screen and then continue rotation
		shipMove.Enable();
		shipMove.transform.position = new Vector3 (shipLocation.x, shipLocation.y, shipLocation.z);
		shipMove.transform.rotation = new Quaternion(shipRotation.x, shipRotation.y, shipRotation.z, shipRotation.w);
	}

	/*
	 * Returns the level
	 */
	public int getLevel(){
		return Level;
	}
}
