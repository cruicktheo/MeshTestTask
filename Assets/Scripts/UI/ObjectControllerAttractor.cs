using UnityEngine;

namespace MeshTestTask
{
    public class ObjectControllerAttractor : MonoBehaviour
    {
        #region Fields
        private const float EASE_TIME = 0.1f;
        [SerializeField] private Transform leftController;
        [SerializeField] private Transform rightController;
        private Transform objectA;
        private Transform objectB;
        private Vector3CriticalDamper objectAAttractDamper;
        private Vector3CriticalDamper objectBAttractDamper;
        private Vector3 initialObjectALocalPosition;
        private Vector3 initialObjectBLocalPosition;
        private bool leftHeld;
        private bool rightHeld;
        private bool isObjectAReset = true;
        private bool isObjectBReset = true;
        private bool isInitialised;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            Events.OnLeftGripHeld += OnLeftGripHeld;
            Events.OnRightGripHeld += OnRightGripHeld;
            Events.OnLeftGripReleased += OnLeftGripReleased;
            Events.OnRightGripReleased += OnRightGripReleased;
        }

        public void LateUpdate()
        {
            if (isInitialised)
            {
                if (leftHeld || (!leftHeld && !isObjectAReset))
                {
                    UpdateLeft();
                }

                if (rightHeld || (!rightHeld && !isObjectBReset))
                {
                    UpdateRight();
                }
            }
        }

        private void OnDisable()
        {
            Events.OnLeftGripHeld -= OnLeftGripHeld;
            Events.OnRightGripHeld -= OnRightGripHeld;
            Events.OnLeftGripReleased -= OnLeftGripReleased;
            Events.OnRightGripReleased -= OnRightGripReleased;
        }
        #endregion

        #region Methods
        public void Initialise(Transform objectA, Transform objectB)
        {
            this.objectA = objectA;
            this.objectB = objectB;
            initialObjectALocalPosition = objectA.position;
            initialObjectBLocalPosition = objectB.position;
            objectAAttractDamper = new Vector3CriticalDamper();
            objectBAttractDamper = new Vector3CriticalDamper();
            objectAAttractDamper.SetEaseTime(EASE_TIME);
            objectBAttractDamper.SetEaseTime(EASE_TIME);

            isInitialised = true;
        }
        #endregion

        #region Implementation
        private void OnLeftGripHeld()
        {
            objectAAttractDamper.Reset(objectA.position);
            leftHeld = true;
        }

        private void OnRightGripHeld()
        {
            objectBAttractDamper.Reset(objectB.position);
            rightHeld = true;
        }

        private void OnLeftGripReleased()
        {
            objectAAttractDamper.Reset(objectA.localPosition);
            leftHeld = false;
        }

        private void OnRightGripReleased()
        {
            objectBAttractDamper.Reset(objectB.localPosition);
            rightHeld = false;
        }

        private void UpdateLeft()
        {
            if (leftHeld)
            {
                isObjectAReset = false;
                objectAAttractDamper.SetTargetValue(leftController.transform.position);
                objectAAttractDamper.Update(Time.deltaTime);
                objectA.transform.position = objectAAttractDamper.GetCurrentValue();
            }
            else
            {
                objectAAttractDamper.SetTargetValue(initialObjectALocalPosition);
                objectAAttractDamper.Update(Time.deltaTime);

                if (!objectAAttractDamper.IsTargetValue())
                {
                    objectA.transform.localPosition = objectAAttractDamper.GetCurrentValue();
                }
                else
                {
                    isObjectAReset = true;
                }
            }
        }

        private void UpdateRight()
        {
            if (rightHeld)
            {
                isObjectBReset = false;
                objectBAttractDamper.SetTargetValue(rightController.transform.position);
                objectBAttractDamper.Update(Time.deltaTime);
                objectB.transform.position = objectBAttractDamper.GetCurrentValue();
            }
            else
            {
                objectBAttractDamper.SetTargetValue(initialObjectBLocalPosition);
                objectBAttractDamper.Update(Time.deltaTime);

                if (!objectBAttractDamper.IsTargetValue())
                {
                    objectB.transform.localPosition = objectBAttractDamper.GetCurrentValue();
                }
                else
                {
                    isObjectBReset = true;
                }
            }
        }
        #endregion
    }
}
