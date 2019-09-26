using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creates and removes platforms for the Planet GameObject
 */
public class ManagePlatforms : MonoBehaviour {

	//Reference to the platform prefab
	public GameObject PlatformPrefab;

	//References to other scripts/GameObjects
	PlanetRotate Planet;
	GameManager gameManager;

	//Transform component
	Transform ThisTransform;

	//Number of platforms to be created
	int NumberOfPlatforms;

	// Use this for initialization
	void Start () {
		ThisTransform = GetComponent<Transform> ();
		Planet = GameObject.Find ("Planet").GetComponent<PlanetRotate> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		//Get the number of platforms from GameManager
		NumberOfPlatforms = gameManager.Platforms;

		//Create platforms
		InitPlatforms ();
	}

	/*
	 * Creates platforms
	 */
	public void InitPlatforms(){
		//Update the number of platforms
		if (NumberOfPlatforms != gameManager.Platforms) {NumberOfPlatforms = gameManager.Platforms; }

		//Return if there are no necessary platforms
		if (NumberOfPlatforms == 0) return;

		//Generate the platforms and place around the planet
		float rotationFactor = 360 / NumberOfPlatforms;
		for (int i = 0; i < NumberOfPlatforms; i++) {
			GameObject go = Instantiate (PlatformPrefab, PlatformPrefab.GetComponent<Transform> ().position, Quaternion.identity);
			go.transform.RotateAround (Planet.getTransform ().position, Vector3.back, rotationFactor * i);

		}

	}

	/*
	 * Destroys all platfoms
	 */
	public void destroyPlatforms(){
		GameObject[] platformList = GameObject.FindGameObjectsWithTag ("Platform");
		foreach (GameObject platform in platformList) {
			//Debug.Log ("DESTROY");
			Destroy (platform);
		}
		
	}

}