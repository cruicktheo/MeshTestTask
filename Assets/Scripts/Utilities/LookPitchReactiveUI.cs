using UnityEngine;

namespace MeshTestTask
{
    public class LookPitchReactiveUI : MonoBehaviour
	{
		#region Enums
		private enum State
		{
			Active,
			Inactive
		}
		#endregion

		#region Fields
		[SerializeField] private Transform userHead;
		[SerializeField] private float inactiveXRotation = 0f;
		[SerializeField] private float activeXRotation	= -45f;
		[SerializeField] private float activeZRotationFollow = 45f;
		[SerializeField] private float movementEaseTime = 0.2f;
		[SerializeField] private float pitchToActivate = 30f;
		[SerializeField] private float pitchToDeactivate = 15f;
	
		private FloatCriticalDamper	_pitchDamper = new FloatCriticalDamper(true);
		private FloatCriticalDamper	_yawDamper = new FloatCriticalDamper(true);
		private State _activeState;
		#endregion
	
		#region Unity Methods
		private void Start()
		{
			_pitchDamper.SetEaseTime(movementEaseTime);
			_yawDamper.SetEaseTime(movementEaseTime);
		}

		private void Update()
	    {
			var headRotation = userHead.rotation.eulerAngles;
			headRotation.x = headRotation.x.ClampToSigned180DegreeRange();
			headRotation.y = headRotation.y.ClampToSigned180DegreeRange();
			
			UpdateDampers();
			SetState(headRotation.x);
			SetDamperValues(headRotation.y);
	
			transform.rotation = Quaternion.Euler(new Vector3(_pitchDamper.GetCurrentValue(), GetAppropriateYaw(headRotation.y), 0f));
	    }
		#endregion
	
		#region Implementation
		private void SetState(float headXRotation)
		{
			if (headXRotation > pitchToActivate)
			{
				_activeState = State.Active;
			}
			else if (headXRotation < pitchToDeactivate)
			{
				_activeState = State.Inactive;
			}
		}
	
		private void SetDamperValues(float headYRotation)
		{
			switch (_activeState)
			{
				case State.Active:
					_pitchDamper.SetTargetValue(activeXRotation);
					CheckYawDelta(headYRotation);
					break;
				case State.Inactive:
					_pitchDamper.SetTargetValue(inactiveXRotation);
					break;
			}
		}
	
		private void UpdateDampers()
		{
			_pitchDamper.Update(Time.deltaTime);
			_yawDamper.Update(Time.deltaTime);
		}
	
		private void CheckYawDelta(float headYRotation)
		{
			var uiYRotation	= transform.rotation.eulerAngles.y.ClampToSigned180DegreeRange();
			
			if (Mathf.Abs(uiYRotation - headYRotation) >= activeZRotationFollow)
			{
				_yawDamper.SetTargetValue(headYRotation);
			}
		}
	
		private float GetAppropriateYaw(float headYRotation)
		{
			switch (_activeState)
			{
				case State.Active:
					return _yawDamper.GetCurrentValue();
				case State.Inactive:
					_yawDamper.SetTargetValue(headYRotation);
					return headYRotation;
				default:
					return headYRotation;
			}
		}
		#endregion
	}
}
