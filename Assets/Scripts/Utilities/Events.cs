using System;

namespace MeshTestTask
{
    public static class Events
    {
        #region Fields
        public static Action OnToggleLissajousAnimator;
        public static Action OnToggleObjectRotator;
        public static Action OnToggleRotationalColor;
        public static Action OnToggleMeshVertexAnimation;
        public static Action OnLeftGripHeld;
        public static Action OnRightGripHeld;
        public static Action OnLeftGripReleased;
        public static Action OnRightGripReleased;
        #endregion
    }

}