using UnityEngine;
using System.Collections.Generic;

namespace MeshTestTask
{
    public class MeshObject : MonoBehaviour
    {
        private List<VisualisationAttribute> visualisationAttributes = new List<VisualisationAttribute>();
        public Mesh Mesh { get; set; }

        public void AddVisualisationAttribute(VisualisationAttribute attribute)
        {
            // Consider adding check so that more than one attribute of the same type cannot be added.
            visualisationAttributes.Add(attribute);
        }

        private void Update()
        {
            foreach(var attribute in visualisationAttributes)
            {
                attribute.Update();
            }
        }

        //private void LateUpdate()
        //{
        //    foreach (var attribute in visualisationAttributes)
        //    {
        //        attribute.LateUpdate();
        //    }
        //}

        private void OnDestroy()
        {
            foreach(var attribute in visualisationAttributes)
            {
                attribute.Cleanup();
            }
        }
    }
}
