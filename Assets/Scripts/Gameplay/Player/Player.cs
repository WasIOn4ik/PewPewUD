using PewCombat;
using PewStorage;
using PewUI;
using System;
using UnityEngine;

namespace PewCore
{
	public class Player : MonoBehaviour, IDamageable
	{
		#region Constants

		private const string LEGS_IDLE = "Legs_Idle";
		private const string LEGS_WALK = "Legs_Walk";
		private const string UPPER_IDLE = "Upper_Idle";
		private const string UPPER_WALK = "Upper_Walk";
		private const string UPPER_IK = "Upper_IK";

		#endregion

		#region Variables

		[Header("Components")]
		[SerializeField] private Transform visualTransform;
		[SerializeField] private Transform weaponSocket;
		[SerializeField] private Animator animator;
		[SerializeField] private CombatSystem combatSystem;
		[SerializeField] private HealthBar healthBar;
		[SerializeField] private Rigidbody2D r2d;

		[Header("Properties")]
		[SerializeField] private float velocity = 10f;
		[SerializeField] private float maxHP = 100f;

		private IPlayerTarget currentTarget;

		public event EventHandler<DamageEventArgs> onHpRatioChanged;

		public static Player Instance { get; private set; }
		public CombatSystem Combat { get { return combatSystem; } }

		private float currentHP;

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

			currentHP = maxHP;

			healthBar.Init(this);
		}

		private void Start()
		{
			healthBar.Init(this);
			combatSystem.OnHaveTargetChanged += CombatSystem_OnHaveTargetChanged;
			GameInputs.Instance.onShootStarted += StartShoot;
			GameInputs.Instance.onShootEnded += EndShoot;

			int weaponID = GameBase.Instance.Stats.GetEquippedWeaponID();

			if (weaponID != 0)
			{
				var weap = GameBase.Instance.ItemsLibrary.GetItem(weaponID) as WeaponSO;
				GameBase.Instance.Inventory.EquipWeapon(weap);
			}

			SetUpperIK(true);
		}

		private void CombatSystem_OnHaveTargetChanged(object sender, CombatSystem.HaveTargetEventArgs e)
		{
			currentTarget = e.target;
			UpdateHandsPosition();
		}

		void Update()
		{
			Vector2 input = GameInputs.Instance.GetMovement();
			Vector2 handledInput = input * Time.deltaTime * velocity;

			HandleRotation(input);

			HandleAnimation(input);

			UpdateHandsPosition();

			r2d.MovePosition(transform.position + new Vector3(handledInput.x, handledInput.y, 0));
		}

		#endregion

		#region Callbacks

		private void StartShoot(object sender, EventArgs e)
		{
			combatSystem.Shoot();
		}

		private void EndShoot(object sender, EventArgs e)
		{
			combatSystem.EndShoot();
		}

		#endregion

		#region Functions

		private void UpdateHandsPosition()
		{
			if (visualTransform.localScale.x == 1)
				weaponSocket.right = currentTarget != null ? currentTarget.GetPosition() - weaponSocket.position : Vector3.right;
			else
				weaponSocket.right = currentTarget != null ? weaponSocket.position - currentTarget.GetPosition() : Vector3.right;

		}

		private void HandleRotation(Vector2 input)
		{
			float rotationThreshold = 0.1f;

			if (input.x > rotationThreshold)
				visualTransform.localScale = Vector3.one;
			else if (input.x < -rotationThreshold)
				visualTransform.localScale = new Vector3(-1, 1, 1);
		}

		private void HandleAnimation(Vector2 input)
		{
			float threshold = 0.05f;

			if (input.magnitude > threshold)
			{
				SetLegsWalk(true);
			}
			else
			{
				SetLegsWalk(false);
			}
		}

		private void SetLegsWalk(bool val)
		{
			animator.SetBool(LEGS_IDLE, !val);
			animator.SetBool(LEGS_WALK, val);
		}

		private void SetUpperIK(bool ik)
		{
			animator.SetBool(UPPER_IK, ik);
			animator.SetBool(UPPER_IDLE, !ik);
		}

		public void ApplyDamage(float damage)
		{
			currentHP -= damage;

			if (currentHP <= 0)
			{
				HandleDeath();
				return;
			}
			else if (currentHP > maxHP)
			{
				currentHP = maxHP;
			}

			onHpRatioChanged?.Invoke(this, new DamageEventArgs() { hpRatio = currentHP / maxHP });
		}

		private void HandleDeath()
		{
			GameBase.Instance.Menus.OpenUI<DeathUI>(MenuType.Death);
		}

		#endregion
	}
}
