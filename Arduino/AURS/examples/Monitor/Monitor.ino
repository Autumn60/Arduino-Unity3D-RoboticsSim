#include "AURS.h"

VirtualSerial vs;

void setup(){
    Serial.begin(250000);

    UnityConnection.Init();
    UnityConnection.Add(&vs);
    UnityConnection.Set();
}

void loop(){
    UnityConnection.Update();
    vs.print("NOW TIME:");
    vs.println(millis()*0.001);
}