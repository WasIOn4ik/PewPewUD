using PewStorage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PewStorage
{
	[Serializable]
	public struct LootDescriptor
	{
		public float chance;
		public ItemSO itemToDrop;
		public List<int> countToDrop;
	}
}

namespace PewComponents
{

	public class LootComponent : MonoBehaviour
	{
		#region Variables

		[SerializeField] private List<LootDescriptor> lootDescriptors = new List<LootDescriptor>();
		[SerializeField] private Pickup pickupPrefab;

		private float maxValueToGenerate;

		#endregion

		#region UnityMessages

		private void Awake()
		{
			if (lootDescriptors.Count > 0)
			{
				foreach (var el in lootDescriptors)
				{
					maxValueToGenerate += el.chance;
				}
			}
		}

		#endregion

		#region Functions

		public void Subscribe(IDropper dropper)
		{
			dropper.OnDrop += Dropper_OnDrop;
		}

		private void Dropper_OnDrop(object sender, EventArgs e)
		{
			var mb = sender as MonoBehaviour;

			if (TryGetLoot(out var itemStack))
			{
				var pickup = Instantiate(pickupPrefab);
				pickup.transform.position = mb.transform.position;
				pickup.SetItem(itemStack);
			}
		}

		public bool TryGetLoot(out ItemStack item)
		{
			item = null;
			if (maxValueToGenerate == 0)
			{
				return false;
			}

			float loot = UnityEngine.Random.Range(0f, maxValueToGenerate);
			float startLoot = 0f;

			foreach (var el in lootDescriptors)
			{
				if (startLoot + el.chance >= loot)
				{
					int countToDrop = el.countToDrop[UnityEngine.Random.Range(0, el.countToDrop.Count)];
					item = new ItemStack() { itemBase = el.itemToDrop, count = countToDrop };
					return true;
				}

				startLoot += el.chance;
			}
			return false;
		}

		#endregion
	}
}
