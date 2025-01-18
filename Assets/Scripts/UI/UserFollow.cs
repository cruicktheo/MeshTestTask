using UnityEngine;

namespace MeshTestTask
{
    public class UserFollow : MonoBehaviour
    {
        #region Fields
        private const float EASE_TIME = 0.1f;
        private const float ROTATION_THRESHOLD_DEGREES = 45f;
        private const float USER_CENTER_DISTANCE_THRESHOLD = 2f;
        [SerializeField] private Transform user;
        private FloatCriticalDamper rotationDamper;
        private Vector3CriticalDamper positionDamper;
        #endregion

        #region Unity Methods
        private void Start()
        {
            rotationDamper = new FloatCriticalDamper();
            positionDamper = new Vector3CriticalDamper();
            rotationDamper.SetEaseTime(EASE_TIME);
            positionDamper.SetEaseTime(EASE_TIME);
        }

        private void Update()
        {
            CheckRotation();
            CheckPosition();
        }
        #endregion

        #region Implementation
        private void CheckRotation()
        {
            var userYRotation = user.transform.rotation.eulerAngles.y.ClampToSigned180DegreeRange();
            var thisYRotation = transform.rotation.eulerAngles.y.ClampToSigned180DegreeRange();

            if (Mathf.Abs(userYRotation - thisYRotation) > ROTATION_THRESHOLD_DEGREES)
            {
                rotationDamper.SetTargetValue(userYRotation);
            }

            UpdateRotation();
        }

        private void CheckPosition()
        {
            var userPosition = user.position;
            var thisPosition = transform.position;

            // Flatten height so that it doesnt recorrect when moving up or down
            userPosition.y = 0f;
            thisPosition.y = 0f;

            if (Vector3.Distance(thisPosition, userPosition) > USER_CENTER_DISTANCE_THRESHOLD)
            {
                positionDamper.SetTargetValue(userPosition);
            }
            
            UpdatePosition();
        }

        private void UpdateRotation()
        {
            rotationDamper.Update(Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, rotationDamper.GetCurrentValue(), 0f));
        }

        private void UpdatePosition()
        {
            positionDamper.Update(Time.deltaTime);
            transform.position = positionDamper.GetCurrentValue();
        }
        #endregion
    }
}
