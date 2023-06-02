using PewStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace PewUI
{
	public class BackpackSlotUI : MonoBehaviour
	{
		#region Variables

		[SerializeField] private Image itemImage;
		[SerializeField] private TMP_Text countText;
		[SerializeField] private Button slotButton;

		private BackpackUI backpackUI;

		private ItemStack item;

		#endregion

		#region UnityMEssages

		private void Awake()
		{
			slotButton.onClick.AddListener(() =>
			{
				backpackUI.currentSlot = this;
				backpackUI.UpdateButtons();
			});
		}

		#endregion

		#region Functions

		public void SetBackpack(BackpackUI backpack)
		{
			backpackUI = backpack;
		}

		public void SetItem(ItemStack newItem)
		{
			item = newItem;
			UpdateDisplay();
		}

		public ItemStack GetItem()
		{
			return item;
		}

		public void UpdateDisplay()
		{
			if (item != null)
			{
				itemImage.gameObject.SetActive(true);
				itemImage.sprite = item.itemBase.inventoryIcon;
				countText.text = item.count.ToString();
				countText.gameObject.SetActive(item.count > 1);
			}
			else
			{
				itemImage.gameObject.SetActive(false);
				countText.gameObject.SetActive(false);
			}
		}

		#endregion
	}
}
