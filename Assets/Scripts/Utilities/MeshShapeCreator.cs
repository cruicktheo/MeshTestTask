using UnityEngine;

namespace MeshTestTask
{
    public static class MeshShapeCreator
    {
        private const float SPHERE_RADIUS = 1f;
        private const int THETA_SEGMENTS = 16;
        private const int PHI_SEGMENTS = 24;

        public static MeshObject CreateSphere(string name, Material material, Vector3 startPosition)
        {
            var mesh = CreateSphereMesh();

            return CreateSphereObject(mesh, name, material, startPosition);
        }

        public static Mesh CreateSphereMesh()
        {
            Vector3[] vertices = new Vector3[2 + (THETA_SEGMENTS - 1) * PHI_SEGMENTS];
            int[] triangles = new int[(PHI_SEGMENTS) * 2 * 3 + (THETA_SEGMENTS - 2) * PHI_SEGMENTS * 2 * 3];

            float thetaStep = Mathf.PI / THETA_SEGMENTS;
            float phiStep = Mathf.PI * 2.0f / PHI_SEGMENTS;
            int vi = 0;
            int ti = 0;
            vertices[vi++] = new Vector3(0, -1, 0) * SPHERE_RADIUS;

            for (int hi = 1; hi < THETA_SEGMENTS; hi++)
            {
                float theta = Mathf.PI - hi * thetaStep;

                for (int pi = 0; pi < PHI_SEGMENTS; pi++)
                {
                    float phi = pi * phiStep;
                    vertices[vi++] = new Vector3(Mathf.Sin(theta) * Mathf.Cos(phi), Mathf.Cos(theta), Mathf.Sin(theta) * Mathf.Sin(phi)) * SPHERE_RADIUS;
                }
            }

            vertices[vi++] = new Vector3(0, 1, 0) * SPHERE_RADIUS;

            for (int pi = 0; pi < PHI_SEGMENTS; pi++)
            {
                triangles[ti++] = 0;
                triangles[ti++] = pi + 1;
                triangles[ti++] = pi != PHI_SEGMENTS - 1 ? (pi + 1) + 1 : 1;
            }

            for (int hi = 0; hi < THETA_SEGMENTS - 2; hi++)
            {
                for (int pi = 0; pi < PHI_SEGMENTS; pi++)
                {
                    int pj = pi != PHI_SEGMENTS - 1 ? pi + 1 : 0;
                    int v00 = pi + hi * PHI_SEGMENTS + 1;
                    int v10 = pj + hi * PHI_SEGMENTS + 1;
                    int v01 = pi + (hi + 1) * PHI_SEGMENTS + 1;
                    int v11 = pj + (hi + 1) * PHI_SEGMENTS + 1;

                    ti = MakeQuad(triangles, ti, v00, v10, v01, v11);
                }
            }

            for (int pi = 0; pi < PHI_SEGMENTS; pi++)
            {
                triangles[ti++] = vertices.Length - 1;
                triangles[ti++] = (pi != PHI_SEGMENTS - 1 ? pi + 1 : 0) + (THETA_SEGMENTS - 2) * PHI_SEGMENTS + 1;
                triangles[ti++] = pi + (THETA_SEGMENTS - 2) * PHI_SEGMENTS + 1;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        private static int MakeQuad(int[] triangles, int ti, int v00, int v10, int v01, int v11)
        {
            triangles[ti] = v00;
            triangles[ti + 1] = triangles[ti + 5] = v01;
            triangles[ti + 2] = triangles[ti + 4] = v10;
            triangles[ti + 3] = v11;
            return ti + 6;
        }

        private static MeshObject CreateSphereObject(Mesh mesh, string name, Material material, Vector3 startPosition)
        {
            var sphereMeshObject = new GameObject(name);
            var renderer = sphereMeshObject.AddComponent<MeshRenderer>();
            var filter = sphereMeshObject.AddComponent<MeshFilter>();
            var sphereMesh = sphereMeshObject.AddComponent<MeshObject>();

            filter.mesh = mesh;
            sphereMesh.Mesh = mesh;
            renderer.material = material;
            sphereMeshObject.transform.position = startPosition;

            return sphereMesh;
        }
    }
}