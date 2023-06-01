using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pew/Weapon")]
public class WeaponSO : ScriptableObject
{
	[Header("Service")]
	public int id;
	public string title;

	[Header("Visual")]
	public Sprite weaponSprite;
	public Sprite weaponBulletSprite;
	public Vector3 frontHandSocketPos;
	public Vector3 backHandSocketPos;
	public bool bUseBackHand;

	[Header("Properties")]
	public float damage;
	public bool bAuto;
	public float fireRate;
	public float bulletVelocity;
}
