#include "AURS.h"

VirtualSerial vs;
VirtualMotor vm;
VirtualEncoder ve(0, 1);

void setup(){
    Serial.begin(250000);

    vm.attach(1);

    UnityConnection.Init();
    UnityConnection.Add(&vs);
    UnityConnection.Add(&vm);
    UnityConnection.Add(&ve);
    UnityConnection.Set();
}

void loop(){
    UnityConnection.Update();
    vm.writeMicroseconds(1700);
    vs.print("ENCODER:");
    vs.println(ve.read());
}