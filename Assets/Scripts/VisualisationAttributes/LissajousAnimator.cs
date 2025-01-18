using UnityEngine;

namespace MeshTestTask
{
    public class LissajousAnimator : VisualisationAttribute
    {
        #region Fields
        private LissajousSettings settings;
        private const float multiplier = 0.001f;
        private Transform transform;
        private Vector3 originalLocalPosition;
        private bool useFixedValues;
        #endregion

        #region Constructor
        public LissajousAnimator(LissajousSettings settings, Transform transform, bool useFixedValues = false)
        {
            this.settings = settings;
            this.transform = transform;
            this.useFixedValues = useFixedValues;
            Events.OnToggleLissajousAnimator += OnToggled;
        }
        #endregion

        #region Methods
        public override void Update()
        {
            if (IsActive)
            {
                transform.localPosition = useFixedValues ? GetUpdatedFixed() : GetUpdatedDynamic();
            }
        }

        public override void Cleanup()
        {
            Events.OnToggleLissajousAnimator -= OnToggled;
        }
        #endregion

        #region Implementation
        private Vector3 GetUpdatedDynamic()
        {
            var position = transform.localPosition;
            position.x += settings.A * Mathf.Sin(settings.a * Time.time + settings.delta) * multiplier;
            position.y += settings.B * Mathf.Sin(settings.b * Time.time) * multiplier;

            return position;
        }

        private Vector3 GetUpdatedFixed()
        {
            var position = transform.localPosition;
            position.x += settings.FixedA * Mathf.Sin(settings.Fixeda * Time.time + settings.FixedDelta) * multiplier;
            position.y += settings.FixedB * Mathf.Sin(settings.Fixedb * Time.time) * multiplier;

            return position;
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
        #endregion
    }
}
