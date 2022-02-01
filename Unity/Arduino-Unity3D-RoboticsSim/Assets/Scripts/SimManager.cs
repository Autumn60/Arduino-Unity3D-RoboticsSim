using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SimManager : MonoBehaviour
{
    [SerializeField]
    ArduinoSerial arduinoSerial;

    [SerializeField]
    private Text portName;

    [SerializeField]
    private Text buttonText;

    private bool isRunnning = false;

    [SerializeField]
    private Module[] modules;

    private int actuatorCount;

    // Start is called before the first frame update
    void Start()
    {
        isRunnning = false;

        actuatorCount = 0;
        Array.Sort(modules, (a, b) => a.moduleType- b.moduleType);
        for(int i = 0;i<modules.Length;i++){
            if(modules[i].moduleType == Module.ModuleType.Actuator)
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

    public void OnButtonClick()
    {
        if (isRunnning)
        {
            arduinoSerial.End();
            buttonText.text = "Start";
        }
        else
        {
            if (portName.text == "") return;
            arduinoSerial.portName = portName.text;
            arduinoSerial.Begin();
            buttonText.text = "Running...";
        }
        isRunnning = !isRunnning;
    }
}
