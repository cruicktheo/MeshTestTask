using UnityEngine;

namespace MeshTestTask
{
    public class RotationalColorizer : VisualisationAttribute
    {
        private Material material;
        private Transform parent;
        private Transform target;
        private Color startColor;

        public RotationalColorizer(Material material, Transform parent, Transform target)
        {
            this.material = material;
            this.parent = parent;
            this.target = target;
            Events.OnToggleRotationalColor += OnToggled;
        }

        public override void Update()
        {
            if (IsActive)
            {
                Vector3 parentWorldForward = parent.transform.TransformDirection(Vector3.forward);
                Vector3 directionToTarget = Vector3.Normalize(target.position - parent.transform.position);

                ColorFromDotProduct(Vector3.Dot(parentWorldForward, directionToTarget));
            }
        }

        public override void Cleanup()
        {
            Events.OnToggleRotationalColor -= OnToggled;
        }

        private void ColorFromDotProduct(float dotProduct)
        {
            // Remap dot product to color value
            var colorValue = Remap(dotProduct, -1f, 1f, 0f, 1f);
            var newColor = new Color(colorValue, 0f, 1f - colorValue, 1f);
            material.color = newColor;
        }

        private float Remap(float value, float currentMin, float currentMax, float newMin, float newMax)
        {
            return newMin + (value - currentMin) * (newMax - newMin) / (currentMax - currentMin);
        }

        private void OnToggled()
        {
            if (IsActive)
            {
                material.color = startColor;
            }
            else
            {
                startColor = material.color;
            }

            ToggleAttributeActive();
        }
    }
}
