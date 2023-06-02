using System.Collections.Generic;
using UnityEngine;


namespace PewUI
{
	[CreateAssetMenu(menuName = "Pew/Menus")]
	public class MenusLibrarySO : ScriptableObject
	{
		#region Variables

		[SerializeField] private List<MenuBase> menus;

		private List<MenuBase> cachedMenus = new List<MenuBase>();

		#endregion

		#region Functions

		public void ClearCached()
		{
			cachedMenus.Clear();
		}

		public T OpenUI<T>(MenuType menu) where T : MenuBase
		{
			foreach (var m in cachedMenus)
			{
				if(m)
				{
					if (m.menuType == menu)
					{
						m.Show();
						return m as T;
					}
				}
			}

			foreach (var m in menus)
			{
				if(m)
				{
					if (m.menuType == menu)
					{
						MenuBase newMenu = Instantiate(m);
						cachedMenus.Add(newMenu);
						newMenu.Show();
						return newMenu as T;
					}
				}
			}

			return null;
		}

		public void CloseUI(MenuType menu)
		{
			foreach (var m in cachedMenus)
			{
				if (m.menuType == menu)
				{
					cachedMenus.Remove(m);
					Destroy(m.gameObject);
					return;
				}
			}
		}

		#endregion
	}
}
