using PewStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewCombat
{
	public class Weapon : MonoBehaviour
	{
		#region Variables

		[SerializeField] protected WeaponSO weaponBase;
		[SerializeField] protected Bullet bulletPrefab;
		[SerializeField] protected Transform barrel;

		private float previousShooted;
		private float shootDelay;
		private BulletDescriptor bulletInfo;

		public WeaponSO WeaponBase { get { return weaponBase; } }

		#endregion

		#region UnityMessages

		protected virtual void Awake()
		{
			shootDelay = 60f / weaponBase.fireRate;

			bulletInfo = new BulletDescriptor()
			{
				damage = weaponBase.damage,
				sender = this,
				sprite = weaponBase.bulletSprite,
				velocity = weaponBase.bulletVelocity,
				bPenetration = weaponBase.bPenetration
			};
		}

		#endregion

		#region Functions

		public virtual void Shoot(bool ForwardDir)
		{
			if (Time.time - previousShooted > shootDelay)
			{
				previousShooted = Time.time;

				var bullet = Instantiate(bulletPrefab);
				bullet.transform.position = barrel.position;
				bulletInfo.velocity = ForwardDir ? weaponBase.bulletVelocity : -weaponBase.bulletVelocity;
				bullet.Init(bulletInfo, barrel.rotation);
			}
		}

		public bool IsAuto()
		{
			return weaponBase.bAuto;
		}

		#endregion
	}
}
