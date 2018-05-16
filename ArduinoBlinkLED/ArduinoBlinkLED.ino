/*
 * ArduinoBlinkLED by Patrick Ignoto
 * 
 * Blinks the on-board LED of an Arduino based 
 * on messages received from an external Unity script.
 * 
 */

//VARIABLE DECLERATIONS

#define SYNC 0x28           //Synchronization character to avoid reading noise as a valid message.
#define BAUD_RATE 115200    //Serial port baud rate. 
#define LED_PIN 13          //Pin 13 is connected to an on-board LED on Arduino UNO.

bool ledState;              //The state of the LED. Either ON or OFF.
char msg[2];                //Container for the message from the Unity Script.


void setup() {
  
  pinMode(LED_PIN, OUTPUT); //Set LED_PIN to a digital output pin.
  Serial.begin(BAUD_RATE);  //Start serial port at specified BAUD_RATE.
  ledState = false;         //Initialize the LED to OFF.

}

void loop() {
  
  //Checks every loop if a new message has come in.
  if (Serial.available()) {
    
    //If it is available, reads the message and stores in the container.
    Serial.readBytes(msg, 2);

    //Checks if it is a valid message (IE has the SYNC character at the front).
    if (msg[0] == SYNC) {
      
      //If it is, sets the value of ledState and turns the led on or off accordingly.
      setLedState();
      writeLedState();
      
    }
  }
}

// Function that sets the boolean ledState to the correct value 
// based on the second character of the message received.
void setLedState() {

  switch (msg[1]) {
    case '0':
      ledState = false;
      break;
    case '1':
      ledState = true;
      break;
    default:
      Serial.println("Error case. No LED state change.");
  }
}

// Function that writes to the digital output to turn the physical 
// LED ON or OFF, based on the current value of ledState. 
void writeLedState() {
  if (ledState) {
    digitalWrite(LED_PIN, HIGH);
  }
  else {
    digitalWrite(LED_PIN, LOW);
  }
}


