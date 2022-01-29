using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimManager : MonoBehaviour
{
    [SerializeField]
    ArduinoSerial arduinoSerial;

    [SerializeField]
    private Modules[] modules;

    private int actuatorCount;

    // Start is called before the first frame update
    void Start()
    {
        actuatorCount = 0;
        Array.Sort(modules, (a, b) => a.moduleType- b.moduleType);
        for(int i = 0;i<modules.Length;i++){
            if(modules[i].moduleType == Modules.ModuleType.Actuator)
                actuatorCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Receive */
        for(int i=0;i<actuatorCount;i++){
            modules[i].SetValue(arduinoSerial.GetReceivedData(modules[i].moduleID));
        }

        /* Send */
        String str = "";
        for(int i=actuatorCount;i<modules.Length;i++){
            str += (modules[i].moduleID + ":" + modules[i].GetValue() + ",");
        }
        str += '\n';
        arduinoSerial.SetSendingData(str);
    }
}
