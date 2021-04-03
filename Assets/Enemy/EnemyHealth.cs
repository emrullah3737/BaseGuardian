using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

  [SerializeField] int maxHitPoints = 5;
  [SerializeField] int difficultyRamp = 1;

  int currentHitPoints = 0;
  Enemy enemy;

  void Start()
  {
    enemy = GetComponent<Enemy>();
  }

  void OnEnable()
  {
    currentHitPoints = maxHitPoints;
  }

  void OnParticleCollision(GameObject other)
  {
    handleProjectileHits();
  }

  void handleProjectileHits()
  {
    if (currentHitPoints > 0)
    {
      currentHitPoints -= 1;
      return;
    }

    DestroyEnemy();
  }

  void DestroyEnemy()
  {
    gameObject.SetActive(false);
    enemy.RewardGold();
    maxHitPoints += difficultyRamp;
  }
}
