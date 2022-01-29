#include "AURS.h"

VirtualMotor vm;

void setup(){
    Serial.begin(250000);

    vm.attach(13);

    UnityConnection.Init();
    UnityConnection.Add(&vm);
    UnityConnection.Set();
}

void loop(){
    vm.writeMicroseconds(1500 + 200 * sin(millis()*0.05*M_PI/180));
    UnityConnection.Update();
}