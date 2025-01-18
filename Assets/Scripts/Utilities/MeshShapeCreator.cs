using UnityEngine;

namespace MeshTestTask
{
    public static class MeshShapeCreator
    {
        #region Fields
        private const string CONE_NAME = "Cone";
        private const float CONE_POSITION_Z = 0.9f;
        private const float SPHERE_RADIUS = 1f;
        private const int THETA_SEGMENTS = 16;
        private const int PHI_SEGMENTS = 24;
        private const int CONE_SEGMENTS = 24;
        private const float CONE_HEIGHT = 0.8f;
        private const float CONE_RADIUS = 0.4f;
        private static readonly Vector3 ConeRotation = new Vector3(90f, 0f, 0f);
        #endregion

        #region Methods
        public static MeshObject CreateSphere(string name, Material material, Vector3 startPosition, bool addForwardCone = false)
        {
            var mesh = CreateSphereMesh();
            Mesh cone = null;

            if (addForwardCone)
            {
                cone = CreateConeMesh();
            }

            return CreateSphereObject(mesh, name, material, startPosition, cone);
        }
        #endregion

        #region Implementation
        private static Mesh CreateSphereMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = CreateSphereVertices();
            mesh.triangles = CreateSphereTriangles(mesh.vertexCount);
            mesh.RecalculateNormals();

            return mesh;
        }

        private static Mesh CreateConeMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = CreateConeVertices();
            mesh.triangles = CreateConeTriangles();
            mesh.RecalculateNormals();

            return mesh;
        }

        private static MeshObject CreateSphereObject(Mesh mesh, string name, Material material, Vector3 startPosition, Mesh cone)
        {
            var sphereMeshObject = new GameObject(name);
            var renderer = sphereMeshObject.AddComponent<MeshRenderer>();
            var filter = sphereMeshObject.AddComponent<MeshFilter>();
            var sphereMesh = sphereMeshObject.AddComponent<MeshObject>();

            filter.mesh = mesh;
            sphereMesh.Mesh = mesh;
            renderer.material = material;
            sphereMeshObject.transform.position = startPosition;

            if (cone != null)
            {
                CreateConeObject(cone, sphereMeshObject.transform, material);
            }

            return sphereMesh;
        }

        private static void CreateConeObject(Mesh mesh, Transform parent, Material material)
        {
            var coneObject = new GameObject(CONE_NAME);
            var coneRenderer = coneObject.AddComponent<MeshRenderer>();
            var coneMeshFilter = coneObject.AddComponent<MeshFilter>();

            coneRenderer.material = material;
            coneMeshFilter.mesh = mesh;
            coneObject.transform.parent = parent;
            coneObject.transform.localPosition = new Vector3(0f, 0f, CONE_POSITION_Z);
            coneObject.transform.rotation = Quaternion.Euler(ConeRotation);
        }

        private static Vector3[] CreateSphereVertices()
        {
            Vector3[] vertices = new Vector3[2 + (THETA_SEGMENTS - 1) * PHI_SEGMENTS];
            float thetaStep = Mathf.PI / THETA_SEGMENTS;
            float phiStep = Mathf.PI * 2.0f / PHI_SEGMENTS;
            int vi = 0;
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

            return vertices;
        }

        private static int[] CreateSphereTriangles(int vertexCount)
        {
            int[] triangles = new int[(PHI_SEGMENTS) * 2 * 3 + (THETA_SEGMENTS - 2) * PHI_SEGMENTS * 2 * 3];
            int ti = 0;

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
                triangles[ti++] = vertexCount - 1;
                triangles[ti++] = (pi != PHI_SEGMENTS - 1 ? pi + 1 : 0) + (THETA_SEGMENTS - 2) * PHI_SEGMENTS + 1;
                triangles[ti++] = pi + (THETA_SEGMENTS - 2) * PHI_SEGMENTS + 1;
            }

            return triangles;
        }

        private static int MakeQuad(int[] triangles, int ti, int v00, int v10, int v01, int v11)
        {
            triangles[ti] = v00;
            triangles[ti + 1] = triangles[ti + 5] = v01;
            triangles[ti + 2] = triangles[ti + 4] = v10;
            triangles[ti + 3] = v11;

            return ti + 6;
        }

        private static Vector3[] CreateConeVertices()
        {
            Vector3[] vertices = new Vector3[CONE_SEGMENTS + 2];
            vertices[0] = new Vector3(0, CONE_HEIGHT, 0);

            for (int i = 0; i < CONE_SEGMENTS; i++)
            {
                float angle = i * Mathf.PI * 2 / CONE_SEGMENTS;
                float x = Mathf.Cos(angle) * CONE_RADIUS;
                float z = Mathf.Sin(angle) * CONE_RADIUS;
                vertices[i + 1] = new Vector3(x, 0, z);
            }

            vertices[CONE_SEGMENTS + 1] = Vector3.zero;

            return vertices;
        }

        private static int[] CreateConeTriangles()
        {
            int[] triangles = new int[CONE_SEGMENTS * 6];
            int triangleIndex = 0;

            for (int i = 0; i < CONE_SEGMENTS; i++)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = i + 1;
                triangles[triangleIndex + 2] = ((i + 1) % CONE_SEGMENTS) + 1;

                triangleIndex += 3;
            }

            for (int i = 0; i < CONE_SEGMENTS; i++)
            {
                triangles[triangleIndex] = CONE_SEGMENTS + 1;
                triangles[triangleIndex + 1] = ((i + 1) % CONE_SEGMENTS) + 1;
                triangles[triangleIndex + 2] = i + 1;

                triangleIndex += 3;
            }

            return triangles;
        }
        #endregion
    }
}