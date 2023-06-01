using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pew/Item")]
public class ItemSO : ScriptableObject
{
	[Header("Service")]
	public int id;
	public string title;

	[Header("Visual")]
	public Sprite icon;

	[Header("Properties")]
	public bool bStackable;
}
