namespace MeshTestTask
{
    public abstract class VisualisationAttribute
    {
        protected bool IsActive { get; set; }

        protected void ToggleAttributeActive()
        {
            IsActive = !IsActive;
        }

        public virtual void Update() { }
        public virtual void Cleanup() { }
    }
}