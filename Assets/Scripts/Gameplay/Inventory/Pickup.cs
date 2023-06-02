using PewCore;
using UnityEngine;

namespace PewStorage
{
	public class Pickup : MonoBehaviour
	{
		#region Variables

		[SerializeField] private SpriteRenderer itemSprite;

		private ItemStack item;

		#endregion

		#region Functions

		public ItemStack GetItem()
		{
			return item;
		}

		public void SetItem(ItemStack it)
		{
			item = it;
			itemSprite.sprite = it.itemBase.inventoryIcon;
		}

		public void PickupItem()
		{
			GameBase.Instance.Inventory.AddItem(item);
		}

		#endregion
	}
}
