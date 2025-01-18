using UnityEngine;
using System.Collections.Generic;

namespace MeshTestTask
{
    public class MeshObject : MonoBehaviour
    {
        #region Fields
        private List<VisualisationAttribute> visualisationAttributes = new List<VisualisationAttribute>();
        public Mesh Mesh { get; set; }
        #endregion

        #region Unity Methods
        private void Update()
        {
            foreach (var attribute in visualisationAttributes)
            {
                attribute.Update();
            }
        }

        private void OnDestroy()
        {
            foreach (var attribute in visualisationAttributes)
            {
                attribute.Cleanup();
            }
        }
        #endregion

        #region Methods
        public void AddVisualisationAttribute(VisualisationAttribute attribute)
        {
            // Consider adding check so that more than one attribute of the same type cannot be added.
            visualisationAttributes.Add(attribute);
        }
        #endregion
    }
}
