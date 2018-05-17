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
	List<string> dropdownOptions = new List<string> ();//Container for the dropdown options.
	private bool dropdownSelected;					//Whether an option has been recently selected from the dropdown.
	public Dropdown dropDown;						//Dropdown from the GUI.
	public Button ledButton;						//Button from the GUI.


	// Use this for initialization
	void Start ()
	{
		//Initialize variables.
		ledState = false;				//LED is off
		previousState = false;			//LED was off previous iteration
		dropdownSelected = false;		//No dropdown option has been selected yet.

		refreshDropdown ();				//Populate the dropdown with options available at start.

		//Initializes message container.
		message = new char[2];
		message [0] = sync;		
		message [1] = boolToChar(ledState);

	}

	// Update is called once per frame
	void Update ()
	{
		//Checks if an option from the dropdown has been selected yet and updates the GUI based on selection.
		if (dropdownSelected) {
			dropdownOptionSelector ();
		}
		//Checks if there has been a change in the state since last iteration.
		if (previousState != ledState) {
			//Writes the message out and updates perviousState.
			writeMessage ();
			buttonTextChange ();
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
			
	}

	void buttonTextChange ()
	{
		if (ledState) {
			ledButton.GetComponentInChildren<Text> ().text = "Led On";
			ledButton.GetComponentInChildren<Text> ().color = UnityEngine.Color.blue;
		}
		else {
			ledButton.GetComponentInChildren<Text> ().text = "Led Off";
			ledButton.GetComponentInChildren<Text> ().color = UnityEngine.Color.red;
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
		//Get file names in /dev/ folder of MacOs
		string[] ports = Directory.GetFiles ("/dev/");
		//Clear any existing options in the dropdown
		dropdownOptions.Clear ();
		//Adds default text for user and a refresh list option.
		dropdownOptions.Add("Select an available serial port... ");
		dropdownOptions.Add ("Refresh List");
		//Adds each valid serial port name.
		foreach (string port in ports) {
			if (port.StartsWith ("/dev/cu.")) {
				dropdownOptions.Add (port);
			}
		}
	}

	//Creates and opens the serial port for communications
	void openSerialPort ()
	{
		//Checks if a serial port already exists and is open. If yes, closes the connection and disposes the object.
		if (serialPort!=null) {
			if (serialPort.IsOpen) { //Written nested because this will throw NullPointerException if it hasn't been created.
				serialPort.Close ();
				serialPort.Dispose ();
			}
		}
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

	//Event handler for when a new option from the dropdown is selected.
	public void dropdownValueChange(){
		dropdownSelected = true;
	}

	//Clears the existing options in the dropdown and populates with the available options.
	void populateDropdown ()
	{
		dropDown.ClearOptions();
		if (dropdownOptions != null && dropDown != null) {
			dropDown.AddOptions (dropdownOptions);
		}
		else {
			Debug.LogWarning ("No serial ports available.");
		}
	}

	//Populates the dropdown with valid options.
	void refreshDropdown ()
	{
		//Gets string list of available ports on MacOS
		getAvailableSerialPorts ();
		//Populates the GUI's dropdown based on previous list
		populateDropdown ();
		//Button should not be interacted with unless a port is selected.
		ledButton.interactable = false;
	}

	//Checks the new dropdown value and refreshes the list or opens a serial port.
	void dropdownOptionSelector ()
	{
		if (dropDown.value == 0) { //default message to user and should not be selected.
			ledButton.interactable = false;
			Debug.LogWarning ("Invalid port name.");
		}
		else if (dropDown.value == 1) {
				refreshDropdown ();
				Debug.Log ("List Refreshed.");
			}
			else {
				port = dropdownOptions [dropDown.value];
				openSerialPort ();
				ledButton.interactable = true;
				buttonTextChange ();
			}
		dropdownSelected = false;
	}
}

