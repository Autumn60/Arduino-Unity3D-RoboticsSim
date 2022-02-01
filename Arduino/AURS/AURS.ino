#include "AURS.h"

VirtualSerial vs;

void setup(){
    Serial.begin(250000);
    UnityConnection.Init();
    UnityConnection.Add(&vs);
    UnityConnection.Set();
}

void loop(){
    vs.println(millis());
    UnityConnection.Update();
}