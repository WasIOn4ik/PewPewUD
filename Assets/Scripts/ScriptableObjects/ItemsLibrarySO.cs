using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pew/ItemsLibrary")]
public class ItemsLibrarySO : ScriptableObject
{
	public List<ItemSO> items;
	public List<WeaponSO> weapons;
}
