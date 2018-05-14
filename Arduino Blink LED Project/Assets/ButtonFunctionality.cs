using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour {

	public bool state;

	// Use this for initialization
	void Start () {
		state = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void ButtonClickFunction () {

		state = !state;

		if (state) {
			Debug.Log ("Button clicked. LED On.");
		} else {
			Debug.Log ("Button clicked. LED Off.");
		}

	}


}
