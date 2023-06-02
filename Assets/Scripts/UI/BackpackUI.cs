using PewCore;
using PewStorage;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PewUI
{
	public class BackpackUI : MenuBase
	{
		#region Variables

		[Header("Components")]
		[SerializeField] private RectTransform itemsContent;
		[SerializeField] private Button destroyButton;
		[SerializeField] private Button dropButton;
		[SerializeField] private Button useButton;
		[SerializeField] private Button closeButton;
		[SerializeField] private Pickup pickupPrefab;
		[SerializeField] private BackpackSlotUI slotPrefab;
		[SerializeField] private BackpackSlotUI currentWeaponSlot;

		[HideInInspector] public BackpackSlotUI currentSlot;
		private List<BackpackSlotUI> cachedSlots = new List<BackpackSlotUI>();

		#endregion

		#region UnityMessages

		private void Awake()
		{
			destroyButton.onClick.AddListener(() =>
			{
				GameBase.Instance.Inventory.RemoveItem(currentSlot.GetItem());
				ResetDisplay();
			});

			dropButton.onClick.AddListener(() =>
			{
				GameBase.Instance.Inventory.RemoveItem(currentSlot.GetItem());

				var pickup = Instantiate(pickupPrefab);
				pickup.SetItem(currentSlot.GetItem());
				pickup.transform.position = Player.Instance.transform.position + Vector3.right * 4;

				ResetDisplay();
			});

			useButton.onClick.AddListener(() =>
			{
				GameBase.Instance.Inventory.TryUseItem(currentSlot.GetItem());

				if (currentSlot.GetItem().count <= 0)
					ResetDisplay();
				else
					currentSlot.UpdateDisplay();
			});

			closeButton.onClick.AddListener(() =>
			{
				Hide();
			});
		}

		#endregion

		#region Overrides

		public override void Show()
		{
			base.Show();
			ResetDisplay();
		}

		#endregion

		#region Functions

		public void UpdateButtons()
		{
			bool slotSelected = currentSlot != null && currentSlot.GetItem() != null;

			dropButton.interactable = slotSelected;
			destroyButton.interactable = slotSelected;
			useButton.interactable = slotSelected && currentSlot.GetItem().itemBase.itemType != ItemType.Craft;
		}

		private void ResetDisplay()
		{
			currentSlot = null;

			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			UpdateButtons();

			int index = 0;
			//Updating slots display
			foreach (var item in GameBase.Instance.Inventory.Items)
			{
				if (cachedSlots.Count > index)
				{
					cachedSlots[index].SetItem(item);
					cachedSlots[index].gameObject.SetActive(true);
					index++;
					continue;
				}

				var slot = Instantiate(slotPrefab, itemsContent);
				cachedSlots.Add(slot);
				slot.SetBackpack(this);
				slot.SetItem(item);
				index++;
			}

			//Cleanup slots
			for (int i = index; i < cachedSlots.Count; i++)
			{
				cachedSlots[i].gameObject.SetActive(false);
			}

			currentWeaponSlot.SetItem(Player.Instance.Combat.CurrentWeapon.WeaponBase.GetStack());
		}

		#endregion
	}
}
