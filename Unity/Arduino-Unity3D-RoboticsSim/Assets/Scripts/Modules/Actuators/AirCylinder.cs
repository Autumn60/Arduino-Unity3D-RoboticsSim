using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCylinder : Module
{
    AirCylinder()
    {
        moduleType = Module.ModuleType.Actuator;
    }

    [SerializeField]
    private ConfigurableJoint joint;

    [SerializeField]
    private float distance = 1.0f;

    [SerializeField]
    private float positionSpring = 50.0f;

    [SerializeField]
    private float positionDamper = 10.0f;

    [SerializeField]
    private float maxsimumForce = 100.0f;

    private SoftJointLimit jointLimit;
    private JointDrive xDrive;

    // Start is called before the first frame update
    void Start()
    {
        joint.autoConfigureConnectedAnchor = false;

        jointLimit.bounciness = 0.0f;
        jointLimit.contactDistance = 0.0f;
        jointLimit.limit = distance * 0.5f;

        xDrive.positionSpring = positionSpring;
        xDrive.positionDamper = positionDamper;
        xDrive.maximumForce = maxsimumForce;

        joint.xDrive = xDrive;

        joint.linearLimit = jointLimit;
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.axis = new Vector3(0, 0, 1);
        joint.connectedAnchor = new Vector3(0, 0, -distance * 0.5f);
        joint.angularXMotion = ConfigurableJointMotion.Locked;
        joint.angularYMotion = ConfigurableJointMotion.Locked;
        joint.angularZMotion = ConfigurableJointMotion.Locked;

        Off();
    }

    void On()
    {
        joint.targetPosition = new Vector3(distance * 0.5f, 0, 0);
    }

    void Off()
    {
        joint.targetPosition = new Vector3(-distance * 0.5f, 0, 0);
    }

    public override void SetValue(string newState)
    {
        if (newState == null) return;
        if (newState == "0")
            Off();
        if (newState == "1")
            On();
    }
}
