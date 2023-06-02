using PewCombat;
using PewCore;
using UnityEngine;

namespace PewStorage
{
	[CreateAssetMenu(menuName = "Pew/Weapon")]
	public class WeaponSO : UsableSO
	{
		public WeaponSO()
		{
			itemType = ItemType.Weapon;
			bStackable = false;
		}

		[Header("WeaponVIsual")]
		public Sprite weaponSprite;
		public Sprite bulletSprite;
		public Vector3 frontHandSocketPos;
		public Vector3 backHandSocketPos;
		public Weapon weaponPrefab;

		[Header("WeaponProperties")]
		public float damage;
		public bool bAuto;
		public float fireRate;
		public float bulletVelocity;
		public bool bPenetration;

		public override void Interact(Player player)
		{
			base.Interact(player);
			GameBase.Instance.Inventory.EquipWeapon(this);
		}
	}
}
