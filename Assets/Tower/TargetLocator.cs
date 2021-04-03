using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
  [SerializeField] Transform weapon;
  [SerializeField] ParticleSystem projectileParticle;
  [SerializeField] float aimRadius = 15f;

  Transform target;

  void Update()
  {
    FindClosestEnemy();
    handleAimToTarget();
  }

  private void FindClosestEnemy()
  {
    Enemy[] enemies = FindObjectsOfType<Enemy>();
    Transform closestEnemy = null;
    float maxDistance = Mathf.Infinity;

    foreach (Enemy enemy in enemies)
    {
      float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
      if (targetDistance < maxDistance)
      {
        closestEnemy = enemy.transform;
        maxDistance = targetDistance;
      }
    }

    target = closestEnemy;
  }

  void handleAimToTarget()
  {
    if (!target) return;

    weapon.LookAt(target);

    float targetDistance = Vector3.Distance(transform.position, target.position);
    bool isAttackActive = targetDistance < aimRadius;
    Attack(isAttackActive);
  }

  void Attack(bool isActive)
  {
    var emission = projectileParticle.emission;
    emission.enabled = isActive;
  }
}
