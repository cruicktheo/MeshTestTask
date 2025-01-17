using UnityEngine;

namespace MeshTestTask
{
    public class ObjectRotator : VisualisationAttribute
    {
        private const float ROTATION_SPEED_RADIANS = 1f;
        private Transform parent;
        private Transform target;
        private Quaternion originalRotation;

        public ObjectRotator(Transform parent, Transform target)
        {
            this.parent = parent;
            this.target = target;
            Events.OnToggleObjectRotator += OnToggled;
        }

        public override void Update()
        {
            if (IsActive)
            {
                var direction = target.position - parent.position;
                var rotationStep = Time.deltaTime * ROTATION_SPEED_RADIANS;
                var newDirection = Vector3.RotateTowards(parent.forward, direction, rotationStep, 0f);

                parent.rotation = Quaternion.LookRotation(newDirection);
            }
        }

        public override void Cleanup()
        {
            Events.OnToggleObjectRotator -= OnToggled;
        }

        private void OnToggled()
        {
            if (IsActive)
            {
                parent.rotation = originalRotation;
            }
            else
            {
                originalRotation = parent.rotation;
            }

            ToggleAttributeActive();
        }
    }
}