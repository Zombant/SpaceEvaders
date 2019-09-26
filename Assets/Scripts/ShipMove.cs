using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that handles the rotation of the ship as well as
 * enabling/disabling it
 */
public class ShipMove : MonoBehaviour {
	
	//PlanetRotate instance
	public PlanetRotate planetRotate;

	//GameManager instance
	GameManager gameManager;

	//RigidBody component
	Rigidbody2D ThisBody;

	//Transform component
	Transform ThisTransform;

	//Speed a which the ship should rotate
	float RotationSpeed;

	// Use this for initialization
	void Start () {
		ThisBody = GetComponent<Rigidbody2D> ();
		ThisTransform = GetComponent<Transform> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		RotationSpeed = gameManager.ShipSpeed;
			
	}

	/*
	 * Rotate the ship every frame
	 */
	void FixedUpdate(){
		//Rotate ship around center
		if(RotationSpeed != gameManager.ShipSpeed){RotationSpeed = gameManager.ShipSpeed;}

		ThisTransform.RotateAround (Vector3.zero, Vector3.forward, RotationSpeed * Time.deltaTime);

	}

	/*
	 * Disable the ship
	 */
	public void Disable(){
		//Debug.Log ("DISABLE");
		gameObject.SetActive (false);
	}

	/*
	 * Enable the ship
	 */
	public void Enable(){
		//Debug.Log ("ENABLE");
		gameObject.SetActive (true);
	}
		
}
