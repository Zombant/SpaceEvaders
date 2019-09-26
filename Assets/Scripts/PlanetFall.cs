using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages the planet's ability to fall off the screen upon
 * death as well as growing back
 */
public class PlanetFall : MonoBehaviour {

    //References to components
	Rigidbody2D ThisBody;
	Transform ThisTransform;

    //References to other GameObjects
	GameObject shipMove;
	GameObject gameManager;

    //References to other scripts
	OpenMenu openMenu;

    //Whether or not the planet should grow
	bool shouldGrow;

    //Whether or not the planet is falling
    bool isFalling;

	// Use this for initialization
	void Start () {
		ThisBody = GetComponent<Rigidbody2D> ();
		ThisTransform = GetComponent<Transform> ();
		shipMove = GameObject.FindGameObjectWithTag ("Player");
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		openMenu = GameObject.FindGameObjectWithTag ("Settings").GetComponent<OpenMenu> ();

        //Planet should not be growing upon startup
		shouldGrow = false;

        //Planet should not be falling upon startup
        isFalling = false;
	}
	
	void FixedUpdate () {
        //Increase the size of the planet
		if (shouldGrow) {
			ThisTransform.localScale += new Vector3 (.1f, .1f, .1f);
			ThisTransform.position = Vector3.zero;
			if(ThisTransform.localScale == new Vector3(1.5f, 1.5f, 1.5f)){
				shouldGrow = false;
				//Debug.Log ("PLANET SHOULDGROW FALSE");

                //Recreate the platforms and re-enable the ship
				gameManager.GetComponent<GameManager> ().reenableStuff ();
                isFalling = false;
			}
		}

	}

    /*
     * Makes the planet fall off the screen
     */
	public void Fall(){

        //Disable the ability to open the menu
        isFalling = true;

		//TODO: Replace with actual animation and set planet to static
		//Make the planet fall
		ThisBody.AddForce (gameManager.transform.up * 800f);
		ThisBody.AddForce (new Vector2 (.5f, .5f) * 200f);
		ThisBody.gravityScale = 10;

        //Disable the planet and move it to the center after 1.5 seconds(when it goes off the screen)
		Invoke ("disable", 1.5f);

	}

    /*
     * Disables the planet, moves it to the center, and scales it to 0
     */
	void disable(){
		//Reset planet for re-growing
		gameObject.SetActive (false);
		ThisBody.gravityScale = 0;
		ThisTransform.localScale = new Vector3 (0, 0, 0);
		ThisTransform.position = Vector3.zero;
		gameObject.SetActive (true);
	}


    /*
     * Regrow the planet after 1.5 seconds.
     * Called from GameManager.GameOver()
     */
    public void Grow(){
		Invoke ("growPlanet", 1.5f);
	}
    //       |
    //       |  Note: Separated so that there is a delay with the use of Invoke()
    //       |
    //       V
	void growPlanet(){
		shouldGrow = true;
	}

    //GETTERS AND SETTERS
    public bool getIsFalling() {
        return isFalling;
    }
}
