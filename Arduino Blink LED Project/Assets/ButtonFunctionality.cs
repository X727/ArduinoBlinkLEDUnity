using System.Collections;
using System.Collections.Generic;
using System;
using System.IO.Ports;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour {

	public bool state;
	private string port;
	private int baudRate;
	private char sync;
	public string ackMsg;
	static SerialPort _serialPort;

	// Use this for initialization
	void Start () {
		state = false;
		port = "/dev/cu.usbmodem1411";
		baudRate = 9600;
		sync = Convert.ToChar(0x28);
		_serialPort = new SerialPort (port, baudRate);
		try{
			_serialPort.Open();
		}
		catch(Exception ex){
			Debug.Log (ex);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void ButtonClickFunction () {

		state = !state;

		try{
			_serialPort.Write(Convert.ToString(sync));
		}
		catch(Exception ex){
			Debug.Log (ex);
		}


		if (state) {
			Debug.Log ("Button clicked. LED On.");
		} else {
			Debug.Log ("Button clicked. LED Off.");
		}

	}


}

