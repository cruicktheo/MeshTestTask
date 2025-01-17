using UnityEngine;

namespace MeshTestTask
{
	public class Vector3CriticalDamper
	{
		#region Fields
		private FloatCriticalDamper	x;
		private FloatCriticalDamper	y;
		private FloatCriticalDamper	z;

		private FloatCriticalDamper[] components = new FloatCriticalDamper[3];
		#endregion

		#region Methods			
		public Vector3CriticalDamper(Vector3 initialValue, float easeTime, bool isAngular = false)
		{
			x = new FloatCriticalDamper(initialValue.x, isAngular);
			y = new FloatCriticalDamper(initialValue.y, isAngular);
			z = new FloatCriticalDamper(initialValue.z, isAngular);

			Initialize();
			
			foreach (var component in components)
			{
				component.SetEaseTime(easeTime);
			}
		}

		public Vector3CriticalDamper(Vector3 initialValue, bool isAngular = false)
		{	
			x = new FloatCriticalDamper(initialValue.x, isAngular);
			y = new FloatCriticalDamper(initialValue.y, isAngular);
			z = new FloatCriticalDamper(initialValue.z, isAngular);
			
			Initialize();

			foreach (var component in components)
			{
				component.SetEaseTime(FloatCriticalDamper.DEFAULT_EASE_TIME);
			}
		}
	
		public Vector3CriticalDamper()
		{
			x = new FloatCriticalDamper(0f);
			y = new FloatCriticalDamper(0f);
			z = new FloatCriticalDamper(0f);
			
			Initialize();
			
			foreach (var component in components)
			{
				component.SetEaseTime(FloatCriticalDamper.DEFAULT_EASE_TIME);
				component.Reset(0f);
			}
		}

		public bool IsTargetValue()
		{
			foreach (var component in components)
			{
				if (component.IsTargetValue() == false)
				{
					return false;
				}
			}

			return true;
		}

		
		public void Reset(Vector3 value)
		{
			x.Reset(value.x);
			y.Reset(value.y);
			z.Reset(value.z);
		}

		public Vector3 GetCurrentValue()
		{
			return new Vector3(x.GetCurrentValue(), y.GetCurrentValue(), z.GetCurrentValue());
		}

		public Vector3 GetTargetValue()
		{
			return new Vector3(x.GetTargetValue(), y.GetTargetValue(), z.GetTargetValue());
		}

		public void SetTargetValue(Vector3 targetValue)
		{
			x.SetTargetValue(targetValue.x);
			y.SetTargetValue(targetValue.y);
			z.SetTargetValue(targetValue.z);
		}

		public float GetEaseTime()
		{
			return x.GetEaseTime();
		}

		public void SetEaseTime(float easeTime)
		{
			foreach (var component in components)
			{
				component.SetEaseTime(easeTime);
			}
		}

		public void Update(float deltaTime)
		{
			foreach (var component in components)
			{
				component.Update(deltaTime);
			}
		}
		#endregion		

		#region Implementation
		private void Initialize()
		{
			components[0] = x;
			components[1] = y;
			components[2] = z;
		}
		#endregion
	}	
}