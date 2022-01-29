using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : Module
{
    Motor()
    {
        moduleType = Module.ModuleType.Actuator;
    }

    [SerializeField]
    private HingeJoint hingejoint;

    [SerializeField]
    private float force;

    [SerializeField]
    private float maxVelocity;

    private JointMotor jointmotor;
    // Start is called before the first frame update
    void Start()
    {
        hingejoint.useMotor = true;
        jointmotor = hingejoint.motor;
        jointmotor.force = force;
        hingejoint.motor = jointmotor;
    }
    public override void SetValue(string newTargetVelocity){
        if(newTargetVelocity==null)return;
        jointmotor.targetVelocity = Mathf.Clamp(float.Parse(newTargetVelocity), -maxVelocity, maxVelocity);
        hingejoint.motor = jointmotor;
    }
}
