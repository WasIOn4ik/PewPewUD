using PewCore;
using PewStorage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PewCombat
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
		[SerializeField] private Transform visualTranform;
		[SerializeField] private Transform weaponSocket;
		[SerializeField] private Transform frontHandController;
		[SerializeField] private Transform backHandController;

		public Weapon CurrentWeapon
		{
			get { return currentWeapon; }
			set
			{
				currentWeapon = value;
				currentWeapon.transform.parent = weaponSocket;
				currentWeapon.transform.localPosition = Vector3.zero;
				currentWeapon.transform.localRotation = Quaternion.identity;
				currentWeapon.transform.localScale = Vector3.one;
				frontHandController.transform.localPosition = currentWeapon.WeaponBase.frontHandSocketPos;
				backHandController.transform.localPosition = currentWeapon.WeaponBase.backHandSocketPos;
				GameBase.Instance.Stats.SetEquippedWeaponID(currentWeapon.WeaponBase.id);
			}
		}

		private List<IPlayerTarget> targets = new List<IPlayerTarget>();

		private IPlayerTarget currentTarget;

		private bool IsShooting;

		#endregion

		#region UnityMessages

		private void Awake()
		{

		}

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
				currentWeapon.Shoot(visualTranform.localScale.x == 1);
			}
		}

		public void OnTriggerEnter2D(Collider2D collision)
		{
			var target = collision.GetComponent<IPlayerTarget>();

			if (target != null)
			{
				targets.Add(target);
			}
		}

		public void OnTriggerExit2D(Collider2D collision)
		{
			var target = collision.GetComponent<IPlayerTarget>();

			if (target != null)
			{
				targets.Remove(target);
			}
		}

		#endregion

		#region Functions

		public void EquipWeapon(WeaponSO weapon)
		{
			if (CurrentWeapon)
			{
				Destroy(CurrentWeapon.gameObject);
			}

			CurrentWeapon = Instantiate(weapon.weaponPrefab);
		}

		public void Shoot()
		{
			if (currentWeapon != null)
			{
				IsShooting = true;
				currentWeapon.Shoot(visualTranform.localScale.x == 1);
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

				float angle = Vector3.Angle(tempVector, transform.right * visualTranform.localScale.x);

				if (tempDistance <= distance && angle < aimAngle)
				{
					distance = tempDistance;
					index = i;
				}
			}

			if (index == -1)
			{
				if (currentTarget != null)
				{
					OnHaveTargetChanged?.Invoke(this, new HaveTargetEventArgs() { target = null });
				}

				currentTarget = null;

				return;
			}

			if (currentTarget != targets[index])
			{
				OnHaveTargetChanged?.Invoke(this, new HaveTargetEventArgs() { target = targets[index] });
			}

			currentTarget = targets[index];
		}

		#endregion
	}
}
