using PewCombat;
using UnityEngine;

namespace PewUI
{
	public class HealthBar : MonoBehaviour
	{
		[SerializeField] private RectTransform fill;

		public void Init(IDamageable target)
		{
			target.onHpRatioChanged += Target_HpChanged;
		}

		private void Target_HpChanged(object sender, DamageEventArgs e)
		{
			fill.localScale = new Vector3(e.hpRatio, 1f, 1f);
		}
	}
}
