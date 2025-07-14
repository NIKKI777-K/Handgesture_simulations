void setup() {
  Serial.begin(9600); // Baud rate must match Unity
}

void loop() {
  int indexValue = analogRead(A0);   // First flex sensor
  int middleValue = analogRead(A1);  // Second flex sensor

  // Send as comma-separated string
  Serial.print(indexValue);
  Serial.print(",");
  Serial.println(middleValue);

  delay(100); 
}
