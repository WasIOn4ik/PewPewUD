using PewStorage;
using PewUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PewStorage
{
	[Serializable]
	public struct ItemSave
	{
		public int id;
		public int count;
	}

	[Serializable]
	public struct SaveGame
	{
		public int lvl;
		public int exp;
		public int equippedID;
		public List<ItemSave> inventory;
	}
}

namespace PewCore
{
	public class GameBase : MonoBehaviour
	{
		#region Constants

		public const string SAVE_FILE = "/Save.pew";

		#endregion

		#region Variables

		[SerializeField] private Inventory inventory = new Inventory();
		[SerializeField] private PlayerStats playerStats = new PlayerStats();

		[SerializeField] private ItemsLibrarySO library;
		[SerializeField] private MenusLibrarySO menus;

		public Inventory Inventory { get { return inventory; } }
		public MenusLibrarySO Menus { get { return menus; } }

		public ItemsLibrarySO ItemsLibrary { get { return library; } }

		public PlayerStats Stats { get { return playerStats; } }

		public static GameBase Instance { get; private set; }

		private BackpackUI cachedBackpack;

		#endregion

		#region UnityMessages

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(gameObject);
			LoadData();
			Menus.ClearCached();

			Application.quitting += Application_quitting;
			Application.focusChanged += Application_focusChanged;
			Application.targetFrameRate = 60;
		}

		#endregion

		#region Callbacks
		private void Application_focusChanged(bool obj)
		{
			if (!obj)
			{
				SaveData();
			}
		}

		private void Application_quitting()
		{
			SaveData();
		}

		#endregion

		#region Functions

		private void LoadData()
		{
			if (File.Exists(Application.persistentDataPath + SAVE_FILE))
			{
				string text = File.ReadAllText(Application.persistentDataPath + SAVE_FILE);

				var saveGame = JsonUtility.FromJson<SaveGame>(text);

				playerStats.SetLevelAndExp(saveGame.lvl, saveGame.exp);
				playerStats.SetEquippedWeaponID(saveGame.equippedID);

				foreach (var savedItem in saveGame.inventory)
				{
					ItemStack item = new ItemStack() { itemBase = library.GetItem(savedItem.id), count = savedItem.count };
					Inventory.AddItem(item);
				}
			}
			else
			{
				SaveData();
			}
		}

		private void SaveData()
		{
			SaveGame sg = GetSave();
			string text = JsonUtility.ToJson(sg);
			File.WriteAllText(Application.persistentDataPath + SAVE_FILE, text);
		}

		private SaveGame GetSave()
		{
			SaveGame sg = new SaveGame();
			sg.lvl = playerStats.GetLevel();
			sg.exp = playerStats.GetExp();
			sg.inventory = new List<ItemSave>();
			sg.equippedID = playerStats.GetEquippedWeaponID();

			foreach (var el in Inventory.Items)
			{
				sg.inventory.Add(new ItemSave() { id = el.itemBase.id, count = el.count });
			}

			return sg;
		}

		#endregion
	}
}
