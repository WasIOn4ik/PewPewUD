using PewCore;
using UnityEngine;


namespace PewUI
{
	public enum MenuType
	{
		Backpack,
		Death
	}

	public class MenuBase : MonoBehaviour
	{
		#region Variables

		[Header("Properties")]
		public MenuType menuType;
		public bool bCacheOnClose = true;

		#endregion

		#region Functions

		public virtual void Show()
		{
			gameObject.SetActive(true);
		}

		public virtual void Hide()
		{
			gameObject.SetActive(false);
		}

		public virtual void Close()
		{
			if (bCacheOnClose)
			{
				Hide();
			}
			else
			{
				GameBase.Instance.Menus.CloseUI(menuType);
			}
		}

		#endregion
	}
}
