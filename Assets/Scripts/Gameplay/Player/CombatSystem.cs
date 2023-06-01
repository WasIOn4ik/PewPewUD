using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewCore
{
	public class CombatSystem : MonoBehaviour
	{
		#region HelperClasses

		public class HaveTargetEventArgs : EventArgs
		{
			public IPlayerTarget target;
		}

		#endregion

		#region Variables

		public event EventHandler<HaveTargetEventArgs> OnHaveTargetChanged;

		[SerializeField] private float aimAngle = 45f;

		[SerializeField] private Weapon currentWeapon;

		private List<IPlayerTarget> targets = new List<IPlayerTarget>();

		private IPlayerTarget currentTarget;

		private bool IsShooting;

		#endregion

		#region UnityMessages

		public void Update()
		{
			if (targets.Count > 0)
			{
				UpdateClosestTarget();
			}
			else
			{
				if (currentTarget != null)
					OnHaveTargetChanged?.Invoke(this, new HaveTargetEventArgs() { target = null });
				currentTarget = null;
			}

			if (IsShooting && currentWeapon != null && currentWeapon.IsAuto())
			{
				currentWeapon.Shoot();
			}
		}

		public void OnTriggerEnter2D(Collider2D collision)
		{
			Debug.Log("Started");
			var target = collision.GetComponent<IPlayerTarget>();

			if (target != null)
			{
				targets.Add(target);
			}
		}

		public void OnTriggerExit2D(Collider2D collision)
		{
			Debug.Log("Ended");
			var target = collision.GetComponent<IPlayerTarget>();

			if (target != null)
			{
				targets.Remove(target);
			}
		}

		#endregion

		#region Functions

		public void Shoot()
		{
			if (currentWeapon != null)
			{
				IsShooting = true;
				currentWeapon.Shoot();
			}
		}

		public void EndShoot()
		{
			IsShooting = false;
		}

		private void UpdateClosestTarget()
		{
			int index = -1;
			float distance = (targets[0].GetPosition() - transform.position).magnitude;
			float tempDistance;
			Vector3 tempVector;

			for (int i = 0; i < targets.Count; i++)
			{
				tempVector = targets[i].GetPosition() - transform.position;
				tempDistance = tempVector.magnitude;

				float angle = Vector3.Angle(tempVector, transform.right * transform.localScale.x);

				if (tempDistance <= distance && angle < aimAngle)
				{
					distance = tempDistance;
					index = i;
				}
			}

			if (index == -1)
			{
				if (currentTarget != null)
					OnHaveTargetChanged?.Invoke(this, new HaveTargetEventArgs() { target = null });

				currentTarget = null;

				return;
			}

			if (currentTarget != targets[index])
				OnHaveTargetChanged?.Invoke(this, new HaveTargetEventArgs() { target = targets[index] });

			currentTarget = targets[index];
		}

		#endregion
	}
}
