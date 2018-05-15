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
      //Serial.println("Sync Rx");
      if(ledOn){
        digitalWrite(LED_PIN, HIGH);
        //Serial.println("LED On");
      }
      else{
        digitalWrite(LED_PIN, LOW);
        //Serial.println("Led Off");
      }
    }
  }

}
