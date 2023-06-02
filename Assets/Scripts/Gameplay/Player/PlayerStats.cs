using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewStorage
{
	public class LevelEventArgs : EventArgs
	{
		public int Level;
		public int XP;
	}

	[Serializable]
	public class PlayerStats
	{
		#region Constants

		private const int startWeaponID = 2;

		#endregion

		#region Variables

		[SerializeField] private int expPerLevel = 100;

		private int level = 1;

		private int exp = 0;

		private int equippedWeaponID = startWeaponID;

		public event EventHandler<LevelEventArgs> onLevelChanged;

		#endregion

		#region Functions

		public int GetLevel()
		{
			return level;
		}

		public int GetExp()
		{
			return exp;
		}

		public float GetExpRatio()
		{
			return (float)exp / expPerLevel;
		}

		public void SetLevelAndExp(int Lvl, int XP)
		{
			level = Lvl;
			exp = XP;
		}

		public void AddExp(int XP)
		{
			exp += XP;
			int levelsDIf = exp / expPerLevel;
			level += levelsDIf;
			exp %= expPerLevel;

			onLevelChanged?.Invoke(this, new LevelEventArgs() { Level = level, XP = exp });
		}

		public void SetEquippedWeaponID(int id)
		{
			equippedWeaponID = id;
		}

		public int GetEquippedWeaponID()
		{
			return equippedWeaponID;
		}

		public void ToDefault()
		{
			level = 1;
			exp = 0;
			equippedWeaponID = startWeaponID;
		}

		#endregion
	}
}
