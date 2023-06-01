using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPlayerTarget, IDamageable
{
	#region Variables

	[SerializeField] private HealthBar healthBar;
	[SerializeField] private Transform aimTarget;
	[SerializeField] private float maxHP;

	private float currentHP;

	public event EventHandler<IDamageable.DamageEventArgs> onHpRatioChanged;

	#endregion

	#region UnityMessages

	private void Awake()
	{
		currentHP = maxHP;
		Debug.Log(gameObject);
		healthBar.Init(this);
	}

	#endregion

	#region Functions

	public Vector3 GetPosition()
	{
		return aimTarget.position;
	}

	public void ApplyDamage(float damage)
	{
		currentHP -= damage;

		if (currentHP <= 0)
		{
			HandleDeath();
			return;
		}

		onHpRatioChanged?.Invoke(this, new IDamageable.DamageEventArgs() { hpRatio = currentHP / maxHP });
	}

	private void HandleDeath()
	{
		Debug.Log("Death");
		//TODO: Ragdoll
		Destroy(gameObject);
	}

	#endregion
}
