using PewUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInputs : MonoBehaviour
{
	[SerializeField] private Joystick movementJoystick;

	[SerializeField] private HoldableButton shootButton;

	public event EventHandler onShootStarted;
	public event EventHandler onShootEnded;

	public static GameInputs Instance { get; private set; }

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

	}

	private void ShootButton_onClickFinished(object sender, EventArgs e)
	{
		Debug.Log("Shooting finished");
		onShootEnded?.Invoke(this, EventArgs.Empty);
	}

	private void ShootButton_onClickStarted(object sender, EventArgs e)
	{
		Debug.Log("Shooting started");
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
}
