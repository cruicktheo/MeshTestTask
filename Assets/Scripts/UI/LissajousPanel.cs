using UnityEngine;
using TMPro;

namespace MeshTestTask
{
    public class LissajousPanel : MonoBehaviour
    {
        private const float FLOAT_STEP = 0.1f;
        private const string FLOAT_STRING_FORMAT = "N1";
        [SerializeField] GameObject root;
        [SerializeField] private LissajousSettings settings;
        [SerializeField] private TMP_Text AValue;
        [SerializeField] private TMP_Text BValue;
        [SerializeField] private TMP_Text aValue;
        [SerializeField] private TMP_Text bValue;
        [SerializeField] private TMP_Text deltaValue;

        private void Start()
        {
            SetValueDisplays();
        }

        private void OnEnable()
        {
            Events.OnToggleLissajousAnimator += ToggleShowRoot;
        }

        private void OnDisable()
        {
            Events.OnToggleLissajousAnimator += ToggleShowRoot;
        }

        public void OnADecremented()
        {
            settings.A--;
            SetValueDisplays();
        }

        public void OnBDecremented()
        {
            settings.B--;
            SetValueDisplays();
        }

        public void OnaDecremented()
        {
            settings.a--;
            SetValueDisplays();
        }

        public void OnbDecremented()
        {
            settings.b--;
            SetValueDisplays();
        }

        public void OnDeltaDecremented()
        {
            settings.delta -= FLOAT_STEP;
            SetValueDisplays();
        }

        public void OnAIncremented()
        {
            settings.A++;
            SetValueDisplays();
        }

        public void OnBIncremented()
        {
            settings.B++;
            SetValueDisplays();
        }

        public void OnaIncremented()
        {
            settings.a++;
            SetValueDisplays();
        }

        public void OnbIncremented()
        {
            settings.b++;
            SetValueDisplays();
        }

        public void OnDeltaIncremented()
        {
            settings.delta += FLOAT_STEP;
            SetValueDisplays();
        }

        private void SetValueDisplays()
        {
            AValue.text = settings.A.ToString();
            BValue.text = settings.B.ToString();
            aValue.text = settings.a.ToString();
            bValue.text = settings.b.ToString();
            deltaValue.text = settings.delta.ToString(FLOAT_STRING_FORMAT);
        }

        private void ToggleShowRoot()
        {
            root.SetActive(!root.activeSelf);
        }
    }
}
