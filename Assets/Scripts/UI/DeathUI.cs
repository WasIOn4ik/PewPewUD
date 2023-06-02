using PewCore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace PewUI
{
	public class DeathUI : MenuBase
	{
		#region Variables

		[SerializeField] private Button playAgainButton;

		#endregion

		#region UnityMessages

		private void Awake()
		{
			playAgainButton.onClick.AddListener(() =>
			{
				GameBase.Instance.Stats.ToDefault();
				GameBase.Instance.Inventory.Clear();
				SceneManager.LoadScene(0);
			});
		}

		#endregion
	}
}
