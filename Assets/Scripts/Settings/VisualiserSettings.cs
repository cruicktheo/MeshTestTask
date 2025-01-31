﻿using UnityEngine;

namespace MeshTestTask
{
    [CreateAssetMenu(fileName = "VisualiserSettings", menuName = "ScriptableObjects/VisualiserSettings")]
    public class VisualiserSettings : ScriptableObject
    {
        #region Fields
        public string ObjectAName;
        public string ObjectBName;
        public Vector3 ObjectAStartPosition;
        public Vector3 ObjectBStartPosition;
        public Vector3 ObjectScale;
        public Material ObjectAMaterial;
        public Material ObjectBMaterial;
        public LissajousSettings LissajousSettings;
        #endregion
    }
}
