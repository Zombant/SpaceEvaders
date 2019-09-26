using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Launches ammo from the Turret on the Ship
 */
public class FireAmmo : MonoBehaviour {

	//Reference to Turret
	GameObject Turret = null;
	Transform TurretTransform = null;

	//Reference to ammo prefab
	public GameObject AmmoPrefab = null;

	//The Transform component
	Transform ThisTransform = null;

	//The button that fires ammo
	public string FireButton;

	// Use this for initialization
	void Start () {
		ThisTransform = GetComponent<Transform> ();
		Turret = GameObject.Find ("Turret");
		TurretTransform = Turret.GetComponent<Transform> ();
	}
	
	/*
	 * Create an Ammo whenever the FireButton is pressed
	 */
	void Update () {
        //Copied Code- Gets the touch position on the screen
        //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //Vector2 touchPos = new Vector2(wp.x, wp.y);

        if (/*gameObject.GetComponent<Collider2D>() != Physics2D.OverlapPoint(touchPos) ||*/ Input.GetButtonDown(FireButton)) {
			GameObject go = Instantiate (AmmoPrefab, TurretTransform.position, TurretTransform.rotation);
		}
	}
}
