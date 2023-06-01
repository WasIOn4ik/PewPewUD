using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
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
	[SerializeField] private Transform weaponSocket;
	[SerializeField] private Animator animator;

	[SerializeField] private float velocity = 10f;

	#endregion

	#region UnityMessages

	void Start()
	{
		GameInputs.Instance.onShoot += Shoot;
	}

	void Update()
	{
		Vector2 input = GameInputs.Instance.GetMovement();
		Vector2 handledInput = input * Time.deltaTime * velocity;

		HandleRotation(input);

		HandleAnimation(input);

		transform.position += new Vector3(handledInput.x, handledInput.y, 0);
	}

	#endregion

	#region Callbacks

	private void Shoot(object sender, EventArgs e)
	{
		Debug.Log("Shooot");
	}

	#endregion

	#region Functions

	private void HandleRotation(Vector2 input)
	{
		if (input.x > 0)
			transform.localScale = Vector3.one;
		else if (input.x < 0)
			transform.localScale = new Vector3(-1, 1, 1);
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

	#endregion
}
