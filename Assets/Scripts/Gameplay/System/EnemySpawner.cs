using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewCombat
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private List<Enemy> enemyVariants;

		[SerializeField] private List<Transform> spawnPoints;

		[SerializeField] private int countToSpawn;

		private void Awake()
		{
			if (countToSpawn > spawnPoints.Count)
			{
				Debug.LogWarning($"Not enough spawn points to spawn enemies {spawnPoints.Count} / {countToSpawn}");
			}

			for (int i = 0; i < countToSpawn && spawnPoints.Count > 0; i++)
			{
				var enemy = enemyVariants[Random.Range(0, enemyVariants.Count)];
				var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

				var spawnedEnemy = Instantiate(enemy, transform);
				spawnedEnemy.transform.position = spawn.position;
				spawnPoints.Remove(spawn);
			}
		}
	}
}
