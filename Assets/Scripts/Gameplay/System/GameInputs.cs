using PewUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInputs : MonoBehaviour
{
	[SerializeField] private Joystick movementJoystick;

	[SerializeField] private Button shootButton;

	public event EventHandler onShoot;

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

		shootButton.onClick.AddListener(() =>
		{
			onShoot?.Invoke(this, EventArgs.Empty);
		});
	}

	public Vector2 GetMovement()
	{
		return movementJoystick.Input;
	}
}
