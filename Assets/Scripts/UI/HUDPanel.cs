using UnityEngine;
    
namespace MeshTestTask
{
    public class HUDPanel : MonoBehaviour
    {
        public void OnToggleLissajousAnimatorPressed()
        {
            Events.OnToggleLissajousAnimator?.Invoke();
        }

        public void OnToggleMeshVertexAnimationPressed()
        {
            Events.OnToggleMeshVertexAnimation?.Invoke();
        }

        public void OnToggleObjectRotatorPressed()
        {
            Events.OnToggleObjectRotator?.Invoke();
        }

        public void OnToggleRotationalColorPressed()
        {
            Events.OnToggleRotationalColor?.Invoke();
        }
    }
}
