using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialMonitor : Module
{
    SerialMonitor()
    {
        moduleID = "VS";
        moduleType = Module.ModuleType.Actuator;
    }

    [SerializeField]
    Monitor monitor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void SetValue(string newText)
    {
        if(newText!=null)
            monitor.Add(newText);
    }
}
