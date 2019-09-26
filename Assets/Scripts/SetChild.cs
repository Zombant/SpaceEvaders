using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sets Object as a child of the Planet object
 */
public class SetChild : MonoBehaviour {

	//Object that will "adopt" this gameObject
	public string ParentObjectName;

	/*
	 * Do the deed
	 */
	void Start () {
		transform.parent = GameObject.Find(ParentObjectName).transform;
	}

}
