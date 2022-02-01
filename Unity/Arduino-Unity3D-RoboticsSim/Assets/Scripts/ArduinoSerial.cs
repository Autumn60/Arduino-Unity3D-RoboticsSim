using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

public class ArduinoSerial : MonoBehaviour {

    [SerializeField]
    public string portName = "COM";

    [SerializeField]
    public int baudrate = 250000;

    [SerializeField]
	private int rate = 100;

    bool sending = false;
    bool receiving = false;

	SerialPort arduino;

    private bool isRunning = false;

    [SerializeField]
    private string sendingData = "";

    Dictionary<string, string> recievedData = new Dictionary<string, string>();
    readonly object lockObject = new object();

	void Start () {
	}

    public bool Begin()
    {
        string[] ports = SerialPort.GetPortNames();
        int index = Array.IndexOf(ports, portName);
        if(index < 0)
        {
            return false;
        }
        arduino = new SerialPort(portName, baudrate);
        arduino.ReadTimeout = 1000;
        arduino.Open();
        isRunning = true;
        return true;
    }

    public void End()
    {
        if(arduino!=null&&arduino.IsOpen)
            arduino.Close();
        isRunning = false;
    }

	void Update () {
        if (!isRunning) return;
        if(sending==false)Task.Run(Send);
        if(receiving==false)Task.Run(Receive);
    }

    void Send() {
		sending = true;

		if (arduino.IsOpen) {
            arduino.Write (sendingData);
		}
        Thread.Sleep(rate);
		sending = false;
	}

    void Receive() {
		receiving = true;
        if (arduino.IsOpen)
        {
            string str;
            str = arduino.ReadLine();

            var subStrings = str.Split(',');
            foreach (var ss in subStrings)
            {
                if (ss == "")
                    continue;
                var pair = ss.Split(':');
                if(pair.Length == 2 && pair[1]!="")
                    recievedData[pair[0]] = pair[1];
            }

        }
        Thread.Sleep(rate);
        receiving = false;
	}

    public void SetSendingData(string data){
        sendingData = data;
    }

    public string GetReceivedData(string key)
    {
        string str;
        lock (lockObject)
        {
            if (recievedData.ContainsKey(key))
            {
                str = recievedData[key];
            }
            else
            {
                str = null;
            }
        }
        return str; 
    }

    void OnApplicationQuit(){
        End();
    }

    void SafeAction(Action action, bool message = true)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            if (message)
            {
                Debug.Log("exeption: " + ex.Message);
            }
        }
    }
}