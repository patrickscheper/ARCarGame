using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Input")]
    public float motorInput;
    public float steerInput;

    public float maxMotorTorque;
    public float maxSteeringAngle;

    private float currentMotorInput;
    private float currentSteeringInput;

    public bool isAddingPositiveTorque;
    public bool isAddingNegativeTorque;

    [Header("References")]
    public List<AxleInfo> axleInfos = new List<AxleInfo>();

    private Rigidbody _rigidbody;
    public Transform centerOfMass;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    public void SetPositiveTorque(bool torque)
    {
        isAddingPositiveTorque = torque;
    }

    public void SetNegativeTorque(bool torque)
    {
        isAddingNegativeTorque = torque;
    }

    public void CheckInput()
    {
#if UNITY_ANDROID
        currentMotorInput = maxMotorTorque * motorInput;
        currentSteeringInput = maxSteeringAngle * steerInput;
#endif

#if UNITY_EDITOR
        currentMotorInput = maxMotorTorque * Input.GetAxis("Vertical");
        currentSteeringInput = maxSteeringAngle * Input.GetAxis("Horizontal");
#endif
    }
    
    void FixedUpdate()
    {

        if (isAddingPositiveTorque && !isAddingNegativeTorque)
            motorInput = Mathf.Lerp(motorInput, 1, 2 * Time.deltaTime);
        else if (!isAddingPositiveTorque && isAddingNegativeTorque)
            motorInput = Mathf.Lerp(motorInput, -1, 2 * Time.deltaTime);
        else if (isAddingPositiveTorque && isAddingNegativeTorque)
            motorInput = Mathf.Lerp(motorInput, 0, 4 * Time.deltaTime);
        else
            motorInput = Mathf.Lerp(motorInput, 0, 4 * Time.deltaTime);


        CheckInput();

        foreach(AxleInfo info in axleInfos)
        {
            if (info.isMotor)
            {
                info.rightWheel.motorTorque = currentMotorInput;
                info.leftWheel.motorTorque = currentMotorInput;
            }
            if (info.isSteering)
            {
                info.rightWheel.steerAngle = currentSteeringInput;
                info.leftWheel.steerAngle = currentSteeringInput;
            }

            MoveVisualWheels(info.rightWheel, info.visualRightWheel);
            MoveVisualWheels(info.leftWheel, info.visualLeftWheel);
        }

    }

    public void MoveVisualWheels(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.transform.position = position;
        wheelTransform.transform.rotation = rotation;
    }
}
[System.Serializable]
public class AxleInfo
{
    public WheelCollider rightWheel;
    public WheelCollider leftWheel;

    public Transform visualRightWheel;
    public Transform visualLeftWheel;

    public bool isMotor;
    public bool isSteering;
}
