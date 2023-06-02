using PewCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PewUI
{
	public class PlayerXPUI : MonoBehaviour
	{
		#region Variables

		[SerializeField] private TMP_Text levelText;
		[SerializeField] private RectTransform expFill;

		#endregion

		#region UnityMEssages

		private void Start()
		{
			expFill.localScale = new Vector3(GameBase.Instance.Stats.GetExpRatio(), 1f, 1f);
			levelText.text = GameBase.Instance.Stats.GetLevel().ToString();
			GameBase.Instance.Stats.onLevelChanged += Stats_onLevelChanged;
		}

		private void OnDestroy()
		{
			GameBase.Instance.Stats.onLevelChanged -= Stats_onLevelChanged;
		}

		#endregion

		#region Functions

		private void Stats_onLevelChanged(object sender, PewStorage.LevelEventArgs e)
		{
			expFill.localScale = new Vector3(GameBase.Instance.Stats.GetExpRatio(), 1f, 1f);
			levelText.text = e.Level.ToString();
		}

		#endregion
	}
}
