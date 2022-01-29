using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encoder : Module
{
    Encoder()
    {
        moduleType = Module.ModuleType.Sensor;
    }

    private enum Axis {
        Y,
        Z
    }

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Axis axis;

    [SerializeField]
    private float PPR = 2048.0f;

    [SerializeField]
    public long pulse = 0;

    private float val;
    private float val_old;

    // Start is called before the first frame update
    void Start()
    {
        switch(axis){
            case Axis.Y:
                val_old = target.localEulerAngles.y;
                break;
            case Axis.Z:
                val_old = target.localEulerAngles.z;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float val_now = 0.0f;
        switch(axis){
            case Axis.Y:
                val_now = target.localEulerAngles.y;
                break;
            case Axis.Z:
                val_now = target.localEulerAngles.z;
                break;
        }
        
        val += Mathf.DeltaAngle(val_old, val_now);
        pulse = (long)(val*PPR/360.0f + 0.5);

        val_old = val_now;
    }

    public override string GetValue(){
        return pulse.ToString();
    }
}
