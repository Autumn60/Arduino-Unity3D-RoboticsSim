#include "AURS.h"

void setup(){
    Serial.begin(250000);
    UnityConnection.Init();

    virtualPinMode(13, OUTPUT);

    UnityConnection.Set();
}

void loop(){
    int trigger = (int)(millis()*0.001*0.5)%2;
    if(trigger == 0){
        virtualDigitalWrite(13, HIGH);
    }else if(trigger == 1){
        virtualDigitalWrite(13, LOW);
    }
    UnityConnection.Update();
}