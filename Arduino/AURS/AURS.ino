#include "AURS.h"

VirtualMotor vm;
VirtualEncoder ve(0, 1);

void setup(){
    Serial.begin(9600);

    vm.attach(5);

    UnityConnection.Init();
    UnityConnection.Add(&vm);
    UnityConnection.Add(&ve);
    UnityConnection.Set();
}

void loop(){
    UnityConnection.Update();
    vm.writeMicroseconds(ve.read()+1500);
}