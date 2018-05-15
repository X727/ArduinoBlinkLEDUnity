using System.Collections;
using System.Collections.Generic;
using System;
using System.IO.Ports;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour {

	public bool state;
	static SerialPort _serialPort;

	// Use this for initialization
	void Start () {
		state = false;
		string port = "";
		int baudRate = 0;
		_serialPort = new SerialPort(port, baudRate);
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
