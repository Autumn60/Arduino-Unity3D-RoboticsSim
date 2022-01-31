#include "AURS.h"

void setup(){
    Serial.begin(250000);
    UnityConnection.Init();

    virtualPinMode(13, OUTPUT);
    virtualDigitalWrite(13, HIGH);

    virtualPinMode(12, OUTPUT);
    virtualDigitalWrite(12, LOW);

    UnityConnection.Set();
}

void loop(){
    virtualAnalogWrite(A1, 255);
    virtualAnalogWrite(A5, 127);
    UnityConnection.Update();
}