using System;
using PewCore;
using System.Collections.Generic;
using UnityEngine;

namespace PewStorage
{
	[Serializable]
	public class Inventory
	{
		#region Variables

		[SerializeField] protected List<ItemStack> items = new List<ItemStack>();

		public IEnumerable<ItemStack> Items { get { return items; } }

		#endregion

		#region Functions

		public void AddItem(ItemStack newItem)
		{
			if (newItem.itemBase.bStackable)
			{
				foreach (ItemStack itemInInventory in items)
				{
					if (newItem.itemBase.id == itemInInventory.itemBase.id)
					{
						itemInInventory.count += newItem.count;
						return;
					}
				}
			}

			//If not stackable or similar item not found
			items.Add(newItem);
		}

		public bool TryUseItem(ItemStack item)
		{
			if (item.itemBase is UsableSO usable)
			{
				usable.Interact(Player.Instance);
				ConsumeItem(item);
				return true;
			}

			return false;
		}

		public void EquipWeapon(WeaponSO weaponSO)
		{
			Debug.Log("Equipping");
			var combat = Player.Instance.Combat;
			if (combat.CurrentWeapon)
			{
				AddItem(combat.CurrentWeapon.WeaponBase.GetStack());
				GameObject.Destroy(combat.CurrentWeapon.gameObject);
			}
			combat.CurrentWeapon = GameObject.Instantiate(weaponSO.weaponPrefab);
		}

		public void RemoveItem(ItemStack item)
		{
			items.Remove(item);
		}

		public void ConsumeItem(ItemStack item)
		{
			item.count--;
			if (item.count <= 0)
			{
				RemoveItem(item);
			}
		}

		public void Clear()
		{
			items.Clear();
		}

		#endregion
	}
}
