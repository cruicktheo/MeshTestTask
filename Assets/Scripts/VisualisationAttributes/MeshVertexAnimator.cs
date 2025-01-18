using UnityEngine;

namespace MeshTestTask
{
    public class MeshVertexAnimator : VisualisationAttribute
    {
        #region Fields
        private Mesh mesh;
        private Vector3[] originalVertices;
        #endregion

        #region Constructor
        public MeshVertexAnimator(Mesh mesh)
        {
            this.mesh = mesh;
            Events.OnToggleMeshVertexAnimation += OnToggled;
        }
        #endregion

        #region Methods
        public override void Update()
        {
            if (IsActive)
            {
                SetVertexNoise();
            }
        }

        public override void Cleanup()
        {
            Events.OnToggleMeshVertexAnimation -= OnToggled;
        }
        #endregion

        #region Implementation
        private void SetVertexNoise()
        {
            var vertices = new Vector3[mesh.vertexCount];

            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = originalVertices[i];
                var noiseX = vertex.x * 1f + Time.time * 1f;
                var noiseY = vertex.y * 1f + Time.time * 1f;

                vertices[i] = vertex + mesh.normals[i] * Mathf.PerlinNoise(noiseX, noiseY) * 0.5f;
            }

            mesh.vertices = vertices;
        }

        private void OnToggled()
        {
            if (IsActive)
            {
                mesh.vertices = originalVertices;
            }
            else
            {
                originalVertices = (Vector3[])mesh.vertices.Clone();
            }

            ToggleAttributeActive();
        }
        #endregion
    }
}
