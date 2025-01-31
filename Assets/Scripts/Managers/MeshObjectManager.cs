using UnityEngine;

namespace MeshTestTask
{
    public class MeshObjectManager : MonoBehaviour
    {
        #region Fields
        private const float REFRESH_RATE = 90f;
        [SerializeField] private GameObject objectParent;
        [SerializeField] private ObjectControllerAttractor objectControllerAttractor;
        [SerializeField] private VisualiserSettings visualiserSettings;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            // Set project at a 90hz refersh rate
            Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(REFRESH_RATE);
        }

        private void Start()
        {
            CreateMeshObjects();
        }
        #endregion

        #region Implementation
        private void CreateMeshObjects()
        {
            var objectA = MeshShapeCreator.CreateSphere(visualiserSettings.ObjectAName, visualiserSettings.ObjectAMaterial, visualiserSettings.ObjectAStartPosition, true);
            var objectB = MeshShapeCreator.CreateSphere(visualiserSettings.ObjectBName, visualiserSettings.ObjectBMaterial, visualiserSettings.ObjectBStartPosition);
            objectA.transform.parent = objectParent.transform;
            objectB.transform.parent = objectParent.transform;
            objectA.transform.localScale = visualiserSettings.ObjectScale;
            objectB.transform.localScale = visualiserSettings.ObjectScale;

            AddVisualisationAttributes(objectA, objectB);
            objectControllerAttractor.Initialise(objectA.transform, objectB.transform);
        }

        private void AddVisualisationAttributes(MeshObject objectA, MeshObject objectB)
        {
            objectA.AddVisualisationAttribute(new LissajousAnimator(visualiserSettings.LissajousSettings, objectA.transform));
            objectB.AddVisualisationAttribute(new LissajousAnimator(visualiserSettings.LissajousSettings, objectB.transform, true));
            objectA.AddVisualisationAttribute(new MeshVertexAnimator(objectA.Mesh));
            objectA.AddVisualisationAttribute(new ObjectRotator(objectA.transform, objectB.transform));
            objectA.AddVisualisationAttribute(new RotationalColorizer(visualiserSettings.ObjectAMaterial, objectA.transform, objectB.transform));
        }
        #endregion
    }
}
