using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour
{
	private string port;
	private bool previousState;
	private const int baudRate = 115200;
	private char sync = Convert.ToChar (0x28);
	private char[] message;
	private string ackMsg;
	private static SerialPort serialPort;

	// Use this for initialization
	void Start ()
	{
		previousState = false;
		LedState = false;
		port = "/dev/cu.usbmodem1411";		//Set our port
		message = new char[1];
		message [0] = sync;		//Build our message

		serialPort = new SerialPort (port, baudRate);
		try {
			serialPort.Open ();
		} catch (IOException ex) {
			Debug.Log ("Unable to open port " + ex.ToString ());
		} catch (Exception ex) {
			Debug.LogError (ex);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (previousState != LedState) {
			writeMessage ();
			previousState = LedState;
		}
	}

	public void ButtonClickFunction ()
	{
		LedState = !LedState;
	}

	private void writeMessage ()
	{
		try {
			if (serialPort != null && serialPort.IsOpen) {
				serialPort.Write (message, 0, message.Length);
			}

		} catch (InvalidOperationException ex) {
			Debug.LogError ("Port is not open" + ex.ToString ());
		} catch (Exception ex) {
			Debug.LogError (ex);
		}


		if (LedState) {
			Debug.Log ("Button clicked. LED On.");
		} else {
			Debug.Log ("Button clicked. LED Off.");
		}
	}

	public bool LedState { get; private set; }

}

