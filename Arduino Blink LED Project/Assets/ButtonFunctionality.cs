/* 
 * ButtonFunctionality by Patrick Ignoto
 * 
 * Opens the serial port connection to the Arduino and sends messages to turn the LED on or off.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctionality : MonoBehaviour
{
	//VARIABLE DECLERATIONS
	private bool ledState;							//State of the LED whether on or off.
	private bool previousState;						//Stores the previous state of the led, to know whether to write the message.
	private string port;							//For serial port name. Need to add dropdown to choose, rather than hardcode.
	private const int baudRate = 115200;			//Serial port baud rate.
	private char sync = Convert.ToChar (0x28);		//Synchronization character to avoid reading noise as a valid message.
	private char[] message;							//Container for message to send via Serial port.
	private static SerialPort serialPort;			//Serial port object.
	List<string> availableSerialPorts = new List<string> ();//Container for the available serial port names.
	public Dropdown dropDown;						//Dropdown from the GUI.
	public Button ledButton;						//Button from the GUI.


	// Use this for initialization
	void Start ()
	{
		//Initialize variables.
		ledState = false;				//LED is off
		previousState = false;			//LED was off previous iteration

		getAvailableSerialPorts ();		//Gets string list of available ports on MacOS
		populateDropdown ();			//Populates the GUI's dropdown based on previous list
		ledButton.interactable = false;	//Button should not be interacted with unless a port is selected.

		//Initializes message container.
		message = new char[2];
		message [0] = sync;		
		message [1] = boolToChar(ledState);

	}

	// Update is called once per frame
	void Update ()
	{
		//Checks if there has been a change in the state since last iteration.
		if (previousState != ledState) {
			//Writes the message out and updates perviousState.
			writeMessage ();
			previousState = ledState;
		}
	}

	//Function called when the button is clicked. 
	//Changes the ledState and updates the message.
	public void ButtonClickFunction ()
	{
		ledState = !ledState;
		message [1] = boolToChar(ledState);

	}

	//Function that writes the updated message to the serial port.
	private void writeMessage ()
	{
		//Tries to write the message if the serial port exists and is open
		try {
			if (serialPort != null && serialPort.IsOpen) {
				serialPort.Write (message, 0, message.Length);
			}

		} catch (InvalidOperationException ex) {
			Debug.LogError ("Port is not open" + ex.ToString ());
		} catch (Exception ex) {
			Debug.LogError (ex);
		}


		if (ledState) {
			Debug.Log ("Button clicked. LED On.");
		} else {
			Debug.Log ("Button clicked. LED Off.");
		}
	}

	//Function that converts the boolean state to a character. 
	//Written because this sort of conversion is unsupported natively in C#.
	private char boolToChar( bool val){

		if (val) {
			return '1';
		} else {
			return '0';
		}
	}

	//Gets the available serial port names on MacOS and writes them to a list.
	private void getAvailableSerialPorts ()
	{
		string[] ports = Directory.GetFiles ("/dev/");
		availableSerialPorts.Add("Select an available serial port: ");
		foreach (string port in ports) {
			if (port.StartsWith ("/dev/cu.")) {
				availableSerialPorts.Add (port);
			}
		}
	}

	//Creates and opens the serial port for communications
	void openSerialPort ()
	{
		//Create the serial port object
		serialPort = new SerialPort (port, baudRate);
		//Try to open it and catch any exceptions.
		try {
			serialPort.Open ();
		}
		catch (IOException ex) {
			Debug.Log ("Unable to open port " + ex.ToString ());
		}
		catch (Exception ex) {
			Debug.LogError (ex);
		}
	}

	//Sets the port name and toggles the button interactibility when a port is chosen.
	public void dropdownValueChange(){
		port = availableSerialPorts[dropDown.value];
		openSerialPort ();
		ledButton.interactable = true;

	}



	void populateDropdown ()
	{
		dropDown.ClearOptions ();
		if (availableSerialPorts != null && dropDown != null) {
			dropDown.AddOptions (availableSerialPorts);
		}
		else {
			Debug.LogWarning ("Can't add list.");
		}
	}
}

