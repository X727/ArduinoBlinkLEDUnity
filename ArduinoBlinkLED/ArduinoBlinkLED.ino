#define SYNC 0x28
#define BAUD_RATE 9600
#define LED_PIN 13

bool ledOn;

void setup() {
  pinMode(LED_PIN, OUTPUT);
  Serial.begin(BAUD_RATE);
  ledOn = false;

}

void loop() {
  if(Serial.available()){
    if(SYNC == Serial.read()){
      ledOn = !ledOn;
      if(ledOn){
        digitalWrite(LED_PIN, HIGH);
      }
      else{
        digitalWrite(LED_PIN, LOW);
      }
    }
  }

}
