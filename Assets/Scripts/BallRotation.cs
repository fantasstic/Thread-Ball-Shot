using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotationElement
    {
        public float Speed;
        public float Duration;
    }

    [SerializeField] private RotationElement[] _rotationPattern;

    private WheelJoint2D _wheelJoint;
    private JointMotor2D _motor;

    private void Awake()
    {
        _wheelJoint = GetComponent<WheelJoint2D>();
        _motor = new JointMotor2D();
        StartCoroutine("PlayRotationPattern");
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;
        while (true)
        {
            yield return new WaitForFixedUpdate();

            _motor.motorSpeed = _rotationPattern[rotationIndex].Speed;
            _motor.maxMotorTorque = 10000;
            _wheelJoint.motor = _motor;

            yield return new WaitForSecondsRealtime(_rotationPattern[rotationIndex].Duration);
            rotationIndex++;
            rotationIndex = rotationIndex < _rotationPattern.Length ? rotationIndex : 0;
        }
    }

}
