using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewCombat
{
	public struct BulletDescriptor
	{
		public Sprite sprite;
		public float velocity;
		public float damage;
		public MonoBehaviour sender;
		public bool bPenetration;
	}

	public class Bullet : MonoBehaviour
	{
		#region Variables

		[SerializeField] private SpriteRenderer bulletSprite;

		private float velocity;
		private float damage;
		private MonoBehaviour sender;
		private bool bPenetration;

		#endregion

		#region Functions

		public void Init(BulletDescriptor descriptor, Quaternion rotation, float lifeTime = 5f)
		{
			bulletSprite.sprite = descriptor.sprite;
			velocity = descriptor.velocity;
			transform.rotation = rotation;
			sender = descriptor.sender;
			damage = descriptor.damage;
			bPenetration = descriptor.bPenetration;
			Destroy(gameObject, lifeTime);
		}

		#endregion

		#region UnityMessages

		private void Update()
		{
			transform.position += transform.right * Time.deltaTime * velocity;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			var target = collision.collider.GetComponent<IDamageable>();

			if (target != null && (object)target != sender)
			{
				target.ApplyDamage(damage);
				if (!bPenetration)
					Destroy(gameObject);
			}
		}

		#endregion
	}
}
