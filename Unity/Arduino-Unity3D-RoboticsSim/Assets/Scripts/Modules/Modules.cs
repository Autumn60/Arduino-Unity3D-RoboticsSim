using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    [SerializeField]
    public string moduleID;

    public enum ModuleType{
        Actuator,
        Sensor
    }

    [SerializeField]
    public ModuleType moduleType;

    public virtual void SetValue(string newValue){

    }

    public virtual string GetValue(){
        string str = "";
        return str;
    }
}
