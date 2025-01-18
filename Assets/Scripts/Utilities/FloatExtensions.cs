namespace MeshTestTask
{
	public static class FloatExtensions
	{
        #region Methods
        public static float ClampToSigned180DegreeRange(this float source)
		{
			if (source > 180f)
			{
				float remainder = source % 180f;
				return (remainder - 180);
			}

			if (source < -180f)
			{
				float remainder = source % 180f;
				return remainder;
			}

			return source;
		}

		public static float GetEquivalentAngleWithOppositeSign(this float source)
		{
			float clampedAngle = source.ClampToSigned180DegreeRange();

			if (clampedAngle > 0)
			{
				return -(360f - clampedAngle);
			}

			return clampedAngle + 360;
		}
        #endregion
    }
}