using PewUI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PewCore
{
	public class GameInputs : MonoBehaviour
	{
		#region Variables

		[SerializeField] private Joystick movementJoystick;

		[SerializeField] private HoldableButton shootButton;

		[SerializeField] private Button backpackButton;

		public event EventHandler onShootStarted;
		public event EventHandler onShootEnded;

		public static GameInputs Instance { get; private set; }

		#endregion

		#region UnityMessages

		private void Awake()
		{
			if (Instance != null)
			{
				Debug.LogError("Duplicating GamInputs");
				Destroy(gameObject);
				return;
			}

			Instance = this;

			shootButton.onClickedStarted += ShootButton_onClickStarted;
			shootButton.onClickedFinished += ShootButton_onClickFinished;

			backpackButton.onClick.AddListener(() =>
			{
				var bp = GameBase.Instance.Menus.OpenUI<BackpackUI>(MenuType.Backpack);
			});
		}

		#endregion

		#region Functions

		private void ShootButton_onClickFinished(object sender, EventArgs e)
		{
			onShootEnded?.Invoke(this, EventArgs.Empty);
		}

		private void ShootButton_onClickStarted(object sender, EventArgs e)
		{
			onShootStarted?.Invoke(this, EventArgs.Empty);
		}

		public bool IsShooting()
		{
			return shootButton.isPressed;
		}

		public Vector2 GetMovement()
		{
			return movementJoystick.Input;
		}

		#endregion
	}
}
