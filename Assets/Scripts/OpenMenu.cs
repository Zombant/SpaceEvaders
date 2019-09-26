using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles the opening and closing of the menu planet
 */
public class OpenMenu : MonoBehaviour {

	//References to other GameObjects and scripts
	PlanetFall planetFall;
	PlanetMove planetMove;
	ShipMove shipMove;
	ManagePlatforms createPlatforms;
	GameManager gameManager;

	//References to the Text gameobject and its Canvas
	public Text levelText;
	GameObject levelCanvas;

	//The Transform component
	Transform ThisTransform;

	//Whether or not the menu is currently open
	bool menuOpen;

	//Whether or not the menu is moving in specified way
	bool shouldGrow;
	bool shouldMoveUp;
	bool shouldShrink;
	bool shouldMoveDown;

    //Whether or not the menu is allowed to switch states
    public bool canSwitch;
    

    // Use this for initialization
    void Start () {
		ThisTransform = GetComponent<Transform> ();
		planetFall = GameObject.FindGameObjectWithTag ("Planet").GetComponent<PlanetFall> ();
		planetMove = GameObject.FindGameObjectWithTag ("Planet").GetComponent<PlanetMove> ();
		shipMove = GameObject.FindGameObjectWithTag ("Player").GetComponent<ShipMove> ();
		createPlatforms = GameObject.FindGameObjectWithTag ("Planet").GetComponent<ManagePlatforms> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		levelCanvas = GameObject.FindGameObjectWithTag ("LevelCanvas");

		//Disable Level text on initial startup
		levelCanvas.SetActive (false);

		//Menu should not be switching states on startup
		menuOpen = false;
		shouldGrow = false;
		shouldMoveUp = false;
		shouldShrink = false;
		shouldMoveDown = false;
        

        //Allowed to switch to the menu when the game first loads(Title Screen)
        canSwitch = true;
    }
	
	// Update is called once per frame
	void Update () {

        //Copied Code- Gets the touch position on the screen
        //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //Vector2 touchPos = new Vector2(wp.x, wp.y);

        if (!menuOpen && canSwitch && !planetFall.getIsFalling()) {
			if (/*gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) */Input.GetButtonDown("Jump")) {
				openMenu ();
				//Debug.Log ("SETTINGS");

                //Make sure the menu cannot be closed immediately after
                canSwitch = false;
			}
		}
		//If the menu is open, allow one click on planet to close
		if (menuOpen && canSwitch && !planetFall.getIsFalling()) {
            if (/*planetFall.gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) */Input.GetButtonDown("Cancel")) {
				closeMenu ();
				//Debug.Log ("RETURN");

                //Make sure the menu cannot be opened immediately after
                canSwitch = false;
            }
		}
        
	}

	/*
	 * Move and scale the menu accordingly
	 */
	void FixedUpdate(){

        //Debug.Log("-------------------" + canSwitch);
		if (shouldGrow) {
			ThisTransform.localScale += new Vector3 (.1f, .1f, .1f);
			if(ThisTransform.localScale == new Vector3(1.5f, 1.5f, 1.5f)){
				shouldGrow = false;
				//Debug.Log ("MENU SHOULDGROW FALSE");
			}
		}
		if (shouldMoveUp) {
			ThisTransform.position = Vector3.MoveTowards (ThisTransform.position, Vector3.zero, .5f);
			if (ThisTransform.position == Vector3.zero) {
				shouldMoveUp = false;
				//Debug.Log ("MENU SHOULDMOVEUP FALSE");
			}
		}
		if (shouldShrink) {
			ThisTransform.localScale -= new Vector3 (.1f, .1f, .1f);
			if(ThisTransform.localScale == new Vector3(.3f, .3f, .3f)){
				shouldShrink = false;
				//Debug.Log ("MENU SHOULDSHRINK FALSE");
			}
		}
		if (shouldMoveDown) {
			ThisTransform.position = Vector3.MoveTowards (ThisTransform.position, new Vector3 (-2.99f, -4.91f, 0f), .5f);
			if (ThisTransform.position == new Vector3 (-2.99f, -4.91f, 0f)) {
				shouldMoveDown = false;
				//Debug.Log ("MENU SHOULDMOVEDOWN FALSE");
			}
		}

        //Allowed to switch on and off the menu when its not currently changing
        if(!shouldGrow && !shouldMoveUp && !shouldMoveDown && !shouldShrink) {
            canSwitch = true;
        }
	}

	/*
	 * Called when the user opens the menu.
	 * Opens the menu
	 */
	void openMenu(){

        //Destroy any surviving Ammo GameObjects
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
        foreach(GameObject ammo in ammos) {
            Destroy(ammo);
        }


		//Debug.Log ("OPEN MENU");

		//Set the state of menuOpen to true
		menuOpen = true;
		//Debug.Log ("MENU MENUOPEN TRUE");

		//shrink and move the planet into the corner
		planetMove.shrinkPlanet();

		//Remove the player
		shipMove.gameObject.SetActive (false);

		//Translate and grow the menu planet
		shouldGrow = true;
		//Debug.Log ("MENU SHOULDGROW TRUE");
		shouldMoveUp = true;
		//Debug.Log ("MENU SHOULDMOVEUP TRUE");

        //Enable the level text on the menu planet and change it to the current level
        levelText.text = gameManager.getLevel().ToString();
		levelCanvas.SetActive (true);
        

	}

	/*
	 * Called when the user closes the menu.
	 * Closes the menu
	 */
	void closeMenu() {
		//Debug.Log ("CLOSE MENU");

		//Set the state of menuOpen to false
		menuOpen = false;
		//Debug.Log ("MENU MENUOPEN FALSE");

		//Move the planet back
		planetMove.replacePlanet();

		//Put the player back if not level 0
		if (gameManager.getLevel () != 0) {
			shipMove.gameObject.SetActive (true);
		}

		//Translate and shrink the menu planet
		shouldShrink = true;
		//Debug.Log ("MENU SHOULDSHRINK TRUE");
		shouldMoveDown = true;
		//Debug.Log ("MENU SHOULDMOVEDOWN TRUE");

		//Disable the level text on the menu planet
		levelCanvas.SetActive (false);

	}
		
    

    //GETTERS AND SETTERS
    //TODO: PUT THESE IN THE VARIABLE DECLARATIONS
    public bool getCanSwitch() {
        return canSwitch;
    }
    public void setCanSwith(bool value) {
        canSwitch = value;
    }
    public bool getMenuOpen() {
        return menuOpen;
    }
}
