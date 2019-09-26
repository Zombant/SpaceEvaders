using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is attached to each Ammo object that is released
 * from the Ship Turret
 */
public class Ammo : MonoBehaviour {

	//This Rigidbody2D
	Rigidbody2D ThisBody = null;

	//GameManager instance
	GameManager gameManager;

	//Speed of Ammo
	public float Thrust;

	//How long each Ammo lives for
	public float LifeTime = 10f;

	void OnEnable () {
		CancelInvoke ();
		Invoke ("Die", LifeTime);
	}

	/*
	 * Initialize Ammo and make it move forward
	 */
	void Start(){
		ThisBody = GetComponent<Rigidbody2D> ();
		ThisBody.AddForce (-transform.up * Thrust);
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}

	/*
	 * This method handles collisions with the planet and platforms
	 */
	void OnTriggerEnter2D (Collider2D col) {
		//For collision with the planet
		if(col.gameObject.CompareTag("Planet")){
			//Debug.Log("PLANET COLLISION");
			//Destroy self
			Destroy(gameObject);
			//Game over
			gameManager.GameOver();
		}
		//For collision with the platforms
		if(col.gameObject.CompareTag("Platform")){
			//Debug.Log ("PLATFORM COLLISION");
			//Destroy self and platform
			Destroy(col.gameObject);
			Destroy (gameObject);
			gameManager.remainingPlatforms--;
		}

	}

	/*
	 * Destroy Ammo when the lifetime is over
	 */
	void Die(){
		Destroy (gameObject);
	}
}
