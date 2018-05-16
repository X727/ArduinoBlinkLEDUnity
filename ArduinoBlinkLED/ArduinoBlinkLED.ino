#define SYNC 0x28
#define BAUD_RATE 115200
#define LED_PIN 13

bool ledOn;
char msg[2];

void setup() {
  pinMode(LED_PIN, OUTPUT);
  Serial.begin(BAUD_RATE);
  ledOn = false;

}

void loop() {
  if (Serial.available()) {
    Serial.readBytes(msg, 2);
    if (msg[0] == SYNC) {
      setLedState();
      writeLedState();
    }
  }
}

void setLedState() {

  switch (msg[1]) {
    case '0':
      ledOn = false;
      break;
    case '1':
      ledOn = true;
      break;
    default:
      Serial.println("Error case. No LED state change.");
  }
}

void writeLedState() {
  if (ledOn) {
    digitalWrite(LED_PIN, HIGH);
    //Serial.println("LED On");
  }
  else {
    digitalWrite(LED_PIN, LOW);
    //Serial.println("Led Off");
  }
}


