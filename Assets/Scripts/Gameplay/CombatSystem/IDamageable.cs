using System;

namespace PewCombat
{
	public class DamageEventArgs : EventArgs
	{
		public float hpRatio;
	}

	public interface IDamageable
	{
		public event EventHandler<DamageEventArgs> onHpRatioChanged;

		public void ApplyDamage(float damage);
	}
}