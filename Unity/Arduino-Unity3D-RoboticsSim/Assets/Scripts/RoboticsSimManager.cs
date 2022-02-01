using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboticsSimManager : MonoBehaviour
{
    [SerializeField]
    private ArduinoSerial arduinoSerial;

    [SerializeField]
    private Monitor systemLogger;

    [SerializeField]
    private Text portNumText;

    [SerializeField]
    private Text baudrateText;

    [SerializeField]
    private Tab connectionButtonTab;
    
    [SerializeField]
    private Module[] modules;

    private int actuatorCount;

    private bool isRunnning = false;

    // Start is called before the first frame update
    void Start()
    {
        isRunnning = false;

        actuatorCount = 0;
        Array.Sort(modules, (a, b) => a.moduleType - b.moduleType);
        for (int i = 0; i < modules.Length; i++)
        {
            if (modules[i].moduleType == Module.ModuleType.Actuator)
                actuatorCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunnning) return;
        /* Receive */
        for (int i = 0; i < actuatorCount; i++)
        {
            modules[i].SetValue(arduinoSerial.GetReceivedData(modules[i].moduleID));
        }

        /* Send */
        String str = "";
        for (int i = actuatorCount; i < modules.Length; i++)
        {
            str += (modules[i].moduleID + ":" + modules[i].GetValue() + ",");
        }
        str += '\n';
        arduinoSerial.SetSendingData(str);
    }

    public void OnConnectionButtonClick()
    {
        if (isRunnning)
        {
            arduinoSerial.End();
            systemLogger.Add("SerialCommunication End");
        }
        else
        {
            bool result = arduinoSerial.Begin();
            if (!result)
            {
                connectionButtonTab.OnButtonClick();
                systemLogger.Add("COM" + portNumText.text + " is not available.");
                return;
            }
            systemLogger.Add("SerialCommunication Start");
        }
        isRunnning = !isRunnning;
    }

    public void UpdatePortNum()
    {
        int portNum = -1;
        bool result = int.TryParse(portNumText.text, out portNum);
        if (result)
        {
            if(portNum >= 0)
            {
                arduinoSerial.portName = "COM" + portNum.ToString();
                systemLogger.Add("Port number is set to "+portNum.ToString());
            }
            else
            {
                systemLogger.Add("Port number must be a positive number.");
            }
        }
        else
        {
            systemLogger.Add("Port number must be an integer.");
        }
    }

    public void UpdateBaudrate()
    {
        int baudrate_ = -1;
        bool result = int.TryParse(baudrateText.text, out baudrate_);
        
        if (result)
        {
            if (baudrate_ >= 0)
            {
                arduinoSerial.baudrate = baudrate_;
                systemLogger.Add("Baudrate is set to " + baudrate_.ToString());
            }
            else
            {
                systemLogger.Add("Baudrate must be a positive number.");
            }
        }
        else
        {
            systemLogger.Add("Baudrate must be an integer.");
        }
    }
}
