using PewCore;
using UnityEngine;


namespace PewStorage
{
	[CreateAssetMenu(menuName = "Pew/UsableItem")]
	public class UsableSO : ItemSO
	{
		public UsableSO()
		{
			itemType = ItemType.Usable;
		}

		public virtual void Interact(Player player)
		{

		}
	}
}
