namespace MeshTestTask
{
    public abstract class VisualisationAttribute
    {
        #region Properties
        protected bool IsActive { get; set; }
        #endregion

        #region Methods
        protected void ToggleAttributeActive()
        {
            IsActive = !IsActive;
        }

        public virtual void Update() { }
        public virtual void Cleanup() { }
        #endregion
    }
}