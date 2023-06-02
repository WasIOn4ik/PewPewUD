using PewComponents;
using PewCore;
using PewUI;
using System;
using UnityEngine;

namespace PewCombat
{
	public class Enemy : MonoBehaviour, IPlayerTarget, IDamageable, IDropper
	{
		#region Constants

		private const string IDLE = "Idle";
		private const string WALK = "Walk";
		private const string ATTACK = "Attack";

		#endregion

		#region Variables

		[Header("Components")]
		[SerializeField] private HealthBar healthBar;
		[SerializeField] private LootComponent lootComponent;
		[SerializeField] private Animator animator;

		[Header("Properties")]
		[SerializeField] private Transform aimTarget;
		[SerializeField] private float maxHP;
		[SerializeField] private float velocity = 5f;
		[SerializeField] private float attackDistance = 2f;
		[SerializeField] private float damage;
		[SerializeField] private float minimalDistance = 0.5f;
		[SerializeField] private int exp = 75;

		private float currentHP;

		private Player playerTarget;

		public event EventHandler<DamageEventArgs> onHpRatioChanged;
		public event EventHandler OnDrop;

		private bool bAttacking;

		#endregion

		#region UnityMessages

		private void Awake()
		{
			currentHP = maxHP;
			healthBar.Init(this);
			lootComponent.Subscribe(this);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var player = other.GetComponent<Player>();

			if (player)
			{
				Debug.Log("FoundPlayer");
				playerTarget = player;
				bAttacking = false;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			var player = other.GetComponent<Player>();

			if (player != null && player == playerTarget)
			{
				playerTarget = null;
				bAttacking = false;
			}
		}

		private void Update()
		{
			MoveToPlayer();
		}

		#endregion

		#region Functions

		public Vector3 GetPosition()
		{
			return aimTarget ? aimTarget.position : Vector3.zero;
		}

		public void ApplyDamage(float damage)
		{
			currentHP -= damage;

			if (currentHP <= 0)
			{
				HandleDeath();
				return;
			}

			onHpRatioChanged?.Invoke(this, new DamageEventArgs() { hpRatio = currentHP / maxHP });
		}

		private void HandleDeath()
		{
			//TODO: Ragdoll
			GameBase.Instance.Stats.AddExp(exp);
			OnDrop?.Invoke(this, EventArgs.Empty);
			Destroy(gameObject);
		}

		private void MoveToPlayer()
		{
			if (playerTarget)
			{
				if (bAttacking)
				{
					return;
				}

				SetIdle(false);
				Vector3 dir = playerTarget.transform.position - transform.position;
				transform.localScale = dir.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
				if (dir.magnitude < attackDistance && !bAttacking)
				{
					animator.SetTrigger(ATTACK);
					bAttacking = true;
				}

				if (dir.magnitude > minimalDistance)
				{
					transform.position += dir.normalized * velocity * Time.deltaTime;
				}
			}
			else
			{
				SetIdle(true);
			}
		}

		private void AttackMoment()
		{
			bAttacking = false;
			if (playerTarget && (playerTarget.transform.position - transform.position).magnitude < attackDistance)
			{
				playerTarget.ApplyDamage(damage);
				SetIdle(false);
			}
		}

		private void SetIdle(bool val)
		{
			animator.SetBool(WALK, !val);
			animator.SetBool(IDLE, val);
		}

		#endregion
	}
}
