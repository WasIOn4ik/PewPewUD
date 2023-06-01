using System;

public interface IDamageable
{
	public class DamageEventArgs : EventArgs
	{
		public float hpRatio;
	}

	public event EventHandler<DamageEventArgs> onHpRatioChanged;

	public void ApplyDamage(float damage);
}