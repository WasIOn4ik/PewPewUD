using UnityEngine;

namespace PewStorage
{
	public enum ItemType
	{
		Usable,
		Weapon,
		Armor,
		Spawnable,
		Craft
	}

	[CreateAssetMenu(menuName = "Pew/Item")]
	public class ItemSO : ScriptableObject
	{
		[Header("Service")]
		public int id;
		public string title;

		[Header("ItemVisual")]
		public Sprite inventoryIcon;

		[Header("ItemProperties")]
		public bool bStackable;
		public ItemType itemType = ItemType.Craft;

		public ItemStack GetStack()
		{
			return new ItemStack() { itemBase = this, count = 1 };
		}
	}
}