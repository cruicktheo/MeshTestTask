using UnityEngine;

namespace MeshTestTask
{
    public class LissajousAnimator : VisualisationAttribute
    {
        private LissajousSettings settings;
        private const float multiplier = 0.001f;
        private Transform transform;
        private Vector3 originalLocalPosition;

        public LissajousAnimator(LissajousSettings settings, Transform transform)
        {
            this.settings = settings;
            this.transform = transform;
            Events.OnToggleLissajousAnimator += OnToggled;
        }

        public override void Update()
        {
            if (IsActive)
            {
                var position = transform.localPosition;
                position.x += settings.A * Mathf.Sin(settings.a * Time.time + settings.delta) * multiplier;
                position.y += settings.B * Mathf.Sin(settings.b * Time.time) * multiplier;

                transform.localPosition = position;
            }
        }

        public override void Cleanup()
        {
            Events.OnToggleLissajousAnimator -= OnToggled;
        }

        private void OnToggled()
        {
            if (IsActive)
            {
                transform.localPosition = originalLocalPosition;
            }
            else
            {
                originalLocalPosition = transform.localPosition;
            }

            ToggleAttributeActive();
        }
    }
}
