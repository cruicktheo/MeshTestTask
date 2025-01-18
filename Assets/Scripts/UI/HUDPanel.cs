using UnityEngine;
using UnityEngine.UI;    

namespace MeshTestTask
{
    public class HUDPanel : MonoBehaviour
    {
        #region Fields
        private readonly Color ButtonOnColor = Color.green;
        private readonly Color ButtonOffColor = Color.white;
        [SerializeField] private Image lissajousAnimatorButton;
        [SerializeField] private Image meshAnimationButton;
        [SerializeField] private Image objectRotatorButton;
        [SerializeField] private Image rotationalColorButton;
        private bool lissajousButtonOn;
        private bool meshAnimationButtonOn;
        private bool objectRotatorButtonOn;
        private bool rotationalColorButtonOn;
        #endregion

        #region Methods
        public void OnToggleLissajousAnimatorPressed()
        {
            lissajousButtonOn = !lissajousButtonOn;
            SetButtonColorFromStatus(lissajousAnimatorButton, lissajousButtonOn);
            Events.OnToggleLissajousAnimator?.Invoke();
        }

        public void OnToggleMeshVertexAnimationPressed()
        {
            meshAnimationButtonOn = !meshAnimationButtonOn;
            SetButtonColorFromStatus(meshAnimationButton, meshAnimationButtonOn);
            Events.OnToggleMeshVertexAnimation?.Invoke();
        }

        public void OnToggleObjectRotatorPressed()
        {
            objectRotatorButtonOn = !objectRotatorButtonOn;
            SetButtonColorFromStatus(objectRotatorButton, objectRotatorButtonOn);
            Events.OnToggleObjectRotator?.Invoke();
        }

        public void OnToggleRotationalColorPressed()
        {
            rotationalColorButtonOn = !rotationalColorButtonOn;
            SetButtonColorFromStatus(rotationalColorButton, rotationalColorButtonOn);
            Events.OnToggleRotationalColor?.Invoke();
        }
        #endregion

        #region Implementation
        private void SetButtonColorFromStatus(Image button, bool isOn)
        {
            button.color = isOn ? ButtonOnColor : ButtonOffColor;
        }
        #endregion
    }
}
