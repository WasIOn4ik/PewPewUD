using PewCore;
using PewStorage;
using UnityEngine;

namespace PewComponents
{
	public class PickupComponent : MonoBehaviour
	{
		public void OnTriggerEnter2D(Collider2D collider)
		{
			var pickup = collider.GetComponent<Pickup>();
			if (pickup)
			{
				GameBase.Instance.Inventory.AddItem(pickup.GetItem());
				Destroy(pickup.gameObject);
			}
		}
	}
}
