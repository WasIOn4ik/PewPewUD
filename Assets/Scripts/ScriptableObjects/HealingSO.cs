using PewCore;
using UnityEngine;

namespace PewStorage
{
	[CreateAssetMenu(menuName = "Pew/Healing")]
	public class HealingSO : UsableSO
	{
		[Header("Properties")]
		public float value;

		public override void Interact(Player player)
		{
			player.ApplyDamage(-value);
		}
	}
}
