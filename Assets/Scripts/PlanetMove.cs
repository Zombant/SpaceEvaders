using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the movement of the planet on opening or
 * closing of the menu screen
 */
public class PlanetMove : MonoBehaviour {

    //References to components
	Rigidbody2D ThisBody;
	Transform ThisTransform;

    //References to other GameObjects
	GameObject shipMove;
	GameObject gameManager;

    //References to other scripts
	OpenMenu openMenu;

	//Whether or not the planet is moving in specified way
	bool shouldShrink;
	bool shouldMoveDown;
	bool shouldGrow;
	bool shouldMoveUp;

	// Use this for initialization
	void Start () {
		ThisBody = GetComponent<Rigidbody2D> ();
		ThisTransform = GetComponent<Transform> ();
		shipMove = GameObject.FindGameObjectWithTag ("Player");
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		openMenu = GameObject.FindGameObjectWithTag ("Settings").GetComponent<OpenMenu> ();

        //Planet should not be switching states on startup
        shouldShrink = false;
		shouldMoveDown = false;
		shouldGrow = false;
		shouldMoveUp = false;
	}

    /*
     * Move and scale the planet accordingly
     */
	void FixedUpdate () {
		if (shouldShrink) {
			ThisTransform.localScale -= new Vector3 (.1f, .1f, .1f);
			if(ThisTransform.localScale == new Vector3(.3f, .3f, .3f)){
				shouldShrink = false;
				//Debug.Log ("PLANET SHOULDSHRINK FALSE");
			}
		}
		if (shouldMoveDown) {
			ThisTransform.position = Vector3.MoveTowards (ThisTransform.position, new Vector3 (-2.99f, -4.91f, 0f), .5f);
			if (ThisTransform.position == new Vector3 (-2.99f, -4.91f, 0f)) {
				shouldMoveDown = false;
				//Debug.Log ("PLANET SHOULDMOVEDOWN FALSE");
			}
		}
		if (shouldGrow) {
			ThisTransform.localScale += new Vector3 (.1f, .1f, .1f);
			if(ThisTransform.localScale == new Vector3(1.5f, 1.5f, 1.5f)){
				shouldGrow = false;
				//Debug.Log ("PLANET SHOULDGROW FALSE");
			}
		}
		if (shouldMoveUp) {
			ThisTransform.position = Vector3.MoveTowards (ThisTransform.position, Vector3.zero, .5f);
			if (ThisTransform.position == Vector3.zero) {
				shouldMoveUp = false;
				//Debug.Log ("PLANET SHOULDMOVEUP FALSE");
			}
		}

	}

    /*
     * Move the planet to the lower left corner and shrink it
     */
	public void shrinkPlanet(){
		shouldShrink = true;
		shouldMoveDown = true;
	}

    /*
     * Move the planet back to the center
     */
	public void replacePlanet(){
		shouldGrow = true;
		shouldMoveUp = true;
	}

}
