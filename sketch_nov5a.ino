const int rainSensorPin = A0;
const int buzzerPin = 13;
const int ledPin = 4;

void setup() {
  pinMode(rainSensorPin, INPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(ledPin, OUTPUT);
  Serial.begin(9600);
}

void loop(){
  int rainSensorValue = digitalRead(rainSensorPin);

  if (!rainSensorValue == LOW) { // LOW indica que se detecta agua
    digitalWrite(buzzerPin, HIGH);
    digitalWrite(ledPin, HIGH);
  } else {
    digitalWrite(buzzerPin, LOW);
    digitalWrite(ledPin, LOW);
  }

  Serial.println(rainSensorValue);
  delay(1000);
  }