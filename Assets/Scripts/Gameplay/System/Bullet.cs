using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletDescriptor
{
	public Sprite sprite;
	public float velocity;
	public float damage;
	public MonoBehaviour sender;
}

public class Bullet : MonoBehaviour
{
	[SerializeField] private SpriteRenderer bulletSprite;

	private float velocity;
	private float damage;
	private MonoBehaviour sender;

	public void Init(BulletDescriptor descriptor, Quaternion rotation, float lifeTime = 5f)
	{
		bulletSprite.sprite = descriptor.sprite;
		velocity = descriptor.velocity;
		transform.rotation = rotation;
		sender = descriptor.sender;
		damage = descriptor.damage;
		Destroy(gameObject, lifeTime);
	}

	private void Update()
	{
		transform.position += transform.right * Time.deltaTime * velocity;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision with " + collision.gameObject.name);
		var target = collision.collider.GetComponent<IDamageable>();
		if (target != null && (object)target != sender)
		{
			target.ApplyDamage(damage);
		}
	}
}
