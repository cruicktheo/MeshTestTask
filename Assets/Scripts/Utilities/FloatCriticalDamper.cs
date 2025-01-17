using UnityEngine;

namespace MeshTestTask
{
	public class FloatCriticalDamper
	{
		#region Fields
		public const float DEFAULT_EASE_TIME = 1f;
		public const float EPSILON = 0.001f;

		private const float	COEFFICIENT_1 = 0.48f;
		private const float	COEFFICIENT_2 = 0.235f;	
		
		private const float	MINIMUM_EASE_TIME = 1f / 1000f;	// 1 millisecond
		private const float	MAXIMUM_EASE_TIME = 60f * 60f;	// 1 hour
		private float easeTime;
		private float velocity;
		private float currentValue;
		private float targetValue;
		private bool isAngular;
		#endregion

		#region Methods
		public FloatCriticalDamper(float initialValue, float easeTime, bool isAngular = false)
		{
			SetEaseTime(easeTime);
			Reset(initialValue);
			this.isAngular = isAngular;
		}
	
		public FloatCriticalDamper(float initialValue, bool isAngular=false)
		{
			SetEaseTime(DEFAULT_EASE_TIME);
			Reset(initialValue);
			this.isAngular = isAngular;
		}
	
		public FloatCriticalDamper(bool isAngular=false)
		{
			SetEaseTime(DEFAULT_EASE_TIME);
			Reset(0f);
			this.isAngular = isAngular;
		}
	
		public bool IsTargetValue()
		{
			return (Mathf.Abs(currentValue - targetValue) < EPSILON);
		}
	
		public void Reset(float value)
		{
			currentValue = value;
			targetValue	= value;
			velocity = 0f;
		}
	
		public float GetCurrentValue()
		{
			return currentValue;
		}
			
		public float GetTargetValue()
		{
			return targetValue;
		}
	
		public void SetTargetValue(float targetValue)
		{
			this.targetValue = targetValue;
			
			if (isAngular)
			{
				this.targetValue = this.targetValue.ClampToSigned180DegreeRange();
				EnsureDeltaAngleBetweenCurrentAndTargetIsLessThan180Degrees();
			}
		}

		public void EnsureDeltaAngleBetweenCurrentAndTargetIsLessThan180Degrees()
		{
			float deltaAngle = targetValue - currentValue;
			
			if (Mathf.Abs(deltaAngle) > 180f)
			{
				float clampedValue = currentValue.ClampToSigned180DegreeRange();
				float newCurrentValue = clampedValue.GetEquivalentAngleWithOppositeSign();
				currentValue = newCurrentValue;
			}
		}
	
		public float GetEaseTime()
		{
			return easeTime;
		}
	
		public void SetEaseTime(float easeTime)
		{
			this.easeTime = Mathf.Clamp(easeTime, MINIMUM_EASE_TIME, MAXIMUM_EASE_TIME);
		}
	
		public void Update(float deltaTime)
		{
			float omega	= 2f / easeTime;
			float x	= omega * deltaTime;
			float exp = 1f / (1f + x + COEFFICIENT_1 * x * x * COEFFICIENT_2 * x * x * x);
		
			float delta	= currentValue - targetValue;
			float temp = (velocity + omega * delta) * deltaTime;
			velocity = (velocity - omega * temp) * exp;
			currentValue = targetValue + (delta + temp) * exp;
		}
		#endregion
	}	
}