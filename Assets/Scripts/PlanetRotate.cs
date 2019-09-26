using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the rotation of the main planet and the settings planet
 */
public class PlanetRotate : MonoBehaviour {

	//Reference to Transform component
	Transform ThisTransform;

	//Speed at which the planet should rotate
	float RotationSpeed;

	//Direction at which the planet should rotate
	public Vector3 direction;

	//Reference to the GameManager object
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		ThisTransform = GetComponent<Transform> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		//Get the speed at which the planet should rotate from the GameManager class
		RotationSpeed = gameManager.PlanetSpeed;

	}

	/*
	 * Rotate the planet based on its type (main, settings, or levels)
	 */
	void FixedUpdate () {
		if (RotationSpeed != gameManager.PlanetSpeed) {
			RotationSpeed = gameManager.PlanetSpeed; 
		}
		if(gameObject.CompareTag("Planet")){
			ThisTransform.RotateAround (ThisTransform.position, Vector3.back, RotationSpeed * Time.deltaTime);
		}
			
		if(!gameObject.CompareTag("Planet")){
			if (gameObject.CompareTag ("Settings")) {
				direction = Vector3.forward;
			} else if (gameObject.CompareTag ("Levels")) {
				direction = Vector3.back;
			}
			ThisTransform.RotateAround (ThisTransform.position, direction, 5f * Time.deltaTime);

		}
	}

	/*
	 * Method returns the Transform component
	 */
	public Transform getTransform(){
		return ThisTransform;
	}
}
