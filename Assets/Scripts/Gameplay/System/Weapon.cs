using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] protected WeaponSO weaponBase;
	[SerializeField] protected Bullet bulletPrefab;
	[SerializeField] protected Transform barrel;

	private float previousShooted;

	private float shootDelay;

	private BulletDescriptor bulletInfo;

	protected virtual void Awake()
	{
		shootDelay = 60f / weaponBase.fireRate;
		bulletInfo = new BulletDescriptor()
		{
			damage = weaponBase.damage,
			sender = this,
			sprite = weaponBase.weaponBulletSprite,
			velocity = weaponBase.bulletVelocity
		};
	}

	public virtual void Shoot()
	{
		if (Time.time - previousShooted > shootDelay)
		{
			Debug.Log("Pew-Pew");
			previousShooted = Time.time;

			var bullet = Instantiate(bulletPrefab);
			bullet.transform.position = barrel.position;
			bullet.Init(bulletInfo, barrel.rotation);
		}
	}

	public bool IsAuto()
	{
		return weaponBase.bAuto;
	}
}
