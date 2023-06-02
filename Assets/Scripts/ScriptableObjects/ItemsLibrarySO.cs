using System.Collections.Generic;
using UnityEngine;

namespace PewStorage
{
	[CreateAssetMenu(menuName = "Pew/ItemsLibrary")]
	public class ItemsLibrarySO : ScriptableObject
	{
		public List<ItemSO> items;

		public ItemSO GetItem(int id)
		{
			foreach (var item in items)
			{
				if (item.id == id)
				{
					return item;
				}
			}
			return null;
		}
	}
}
