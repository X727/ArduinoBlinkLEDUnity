#Arduino LED Blink Project

A unity scene with a button that will turn the Arduino UNO’s on-board LED on or off. Includes a dropdown list to select which serial port is to be used. 
Developed and tested on Mid 2012 Macbook Pro running MacOS v.10.11.6.

##Requirements

Requires MacOS computer, Arduino UNO, Unity v2017.3.1 and Arduino IDE.

##Installing

###Upload Arduino Firmware

1.Open the Arduino code ArduinoBlinkLED.ino in the Arduino IDE.
1.Connect your Arduino UNO with a USB cable to your computer.
1.Set the correct board and port name in the “Tools” menu.
1.Push the “Upload” button to upload the Arduino firmware to the Arduino UNO.

###Open Unity project

1. Open the Unity project and scene by opening the Arduino Blink LED Project/Assets/Arduino Blink LED Unity.unity file.
1. Press the “Run” button at the top to start the program.

##Using the program

1. At the program start, use the dropdown list to select a serial port. If you don’t see the serial port name of your Arduino, ensure the Arduino is connected properly and press “Refresh List” to refresh the list.
1. Allow a few seconds to establish the connection, the LED will flash when it is done.
1. Press the button in the centre of the scene. This will change the text from “LED Off” to “LED On” and the on-board LED of the Arduino Uno will light up.
1. Press it again to turn it off again. 

##Important

Will only work on the Arduino Uno, and on Mac operating systems. Not available on Windows at the moment. 

