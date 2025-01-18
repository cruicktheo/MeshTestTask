using UnityEngine;

namespace MeshTestTask
{
    [CreateAssetMenu(fileName = "LissajousSettings", menuName = "ScriptableObjects/LissajousSettings")]
    public class LissajousSettings : ScriptableObject
    {
        #region Fields
        public float A = 4f;
        public float B = 4f;
        public float a = 5f;
        public float b = 4f;
        public float delta = 2.3f;

        public float FixedA = 2f;
        public float FixedB = 8f;
        public float Fixeda = 2f;
        public float Fixedb = 5f;
        public float FixedDelta = 3.3f;
        #endregion
    }
}
