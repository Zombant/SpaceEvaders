using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMenu : MonoBehaviour {

    //Speed of rotation
    public float RotationSpeed;

    //References to components
    Transform ThisTransform;

	// Use this for initialization
	void Start () {
        ThisTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //Location of the user tap
        //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //Vector2 touchPos = new Vector2(wp.x, wp.y);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.transform.gameObject.tag == "Settings" && ThisTransform.position == Vector3.zero) {
            Debug.Log("THERE IS NO BUG");
            Vector3 direction = (Input.mousePosition - ThisTransform.position).normalized;
            direction.x = 0f;
            direction.y = 0f;
            ThisTransform.rotation = Quaternion.Slerp(ThisTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * RotationSpeed);
            
        }
	}
}
