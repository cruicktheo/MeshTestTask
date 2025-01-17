using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

namespace MeshTestTask
{
    public class InputHandler : MonoBehaviour
    {
        private const float GRIP_TIME_SECONDS = 0.3f;
        [SerializeField] private InputDevice leftController;
        [SerializeField] private InputDevice rightController;
        private bool leftWasGripped;
        private bool rightWasGripped;
        private float leftGripHoldStartTime;
        private float rightGripHoldStartTime;

        private void Update()
        {
            if (!leftController.isValid || !rightController.isValid)
            {
                InitialiseControllers();
            }
            else
            {
                CheckInputs();
            }
        }

        private void CheckInputs()
        {
            if (leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftGripOn))
            {
                if (leftGripOn)
                {
                    if (!leftWasGripped)
                    {
                        leftGripHoldStartTime = Time.time;
                        leftWasGripped = true;
                    }
                    else if (Time.time - leftGripHoldStartTime > GRIP_TIME_SECONDS)
                    {
                        Events.OnLeftGripHeld?.Invoke();
                        leftGripHoldStartTime = float.MaxValue;
                    }
                }
                else
                {
                    if (leftWasGripped)
                    {
                        Events.OnLeftGripReleased?.Invoke();
                        leftWasGripped = false;
                    }
                }
            }

            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightGripOn))
            {
                if (rightGripOn)
                {
                    if (!rightWasGripped)
                    {
                        rightGripHoldStartTime = Time.time;
                        rightWasGripped = true;
                    }
                    else if (Time.time - rightGripHoldStartTime > GRIP_TIME_SECONDS)
                    {
                        Events.OnRightGripHeld?.Invoke();
                        rightGripHoldStartTime = float.MaxValue;
                    }
                }
                else
                {
                    if (rightWasGripped)
                    {
                        Events.OnRightGripReleased?.Invoke();
                        rightWasGripped = false;
                    }
                }
            }
        }

        private void InitialiseControllers()
        {
            if (!leftController.isValid)
            {
                InitialiseController(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref leftController);
            }

            if (!rightController.isValid)
            {
                InitialiseController(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref rightController);
            }
        }

        private void InitialiseController(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

            if (devices.Count > 0)
            {
                inputDevice = devices[0];
            }
        }
    }

}