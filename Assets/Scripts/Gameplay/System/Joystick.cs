using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

namespace PewUI
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField] private RectTransform stick;
		[SerializeField] private RectTransform background;

		[SerializeField] private float offset;

		public Vector2 Input { get; private set; }

		public void OnDrag(PointerEventData eventData)
		{
			Vector2 tempInput = Vector2.zero;

			float xSize = background.sizeDelta.x / 2;
			float ySize = background.sizeDelta.y / 2;

			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out tempInput))
			{
				tempInput.x /= xSize;
				tempInput.y /= ySize;
				Input = tempInput;
				Input = Input.magnitude > 1f ? Input.normalized : Input;

				Debug.Log($"{xSize} , {ySize}, {tempInput}, {Input}");
				stick.anchoredPosition = new Vector2(Input.x * xSize, Input.y * ySize);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			OnDrag(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Input = Vector2.zero;

			stick.anchoredPosition = Vector2.zero;
		}
	}
}


