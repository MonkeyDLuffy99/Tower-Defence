using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float attackRange = 10f;
    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem projectileParticle;
    
    public Waypoint baseWaypoint;

    Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
      SetTargetEnemy();
      if (targetEnemy) {
        objectToPan.LookAt(targetEnemy);
        FireAtEnemy();
      }
      else {
        Shoot(false);
      }
    }

    private void SetTargetEnemy() {
      var sceneEnemies = FindObjectsOfType<EnemyDamage>();
      if (sceneEnemies.Length == 0) {
        return;
      }

      Transform closestEnemy = sceneEnemies[0].transform;

      foreach(EnemyDamage testEnemy in sceneEnemies) {
        closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
      }

      targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB) {
      var distToA = Vector3.Distance(transform.position, transformA.position);
      var distToB = Vector3.Distance(transform.position, transformB.position);

      if (distToA < distToB) {
        return transformA;
      }
      return transformB;
    }

    private void FireAtEnemy() {
      float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
      
        if (distanceToEnemy <= attackRange) {
          Shoot(true);
        }
        else {
          Shoot(false);
        }
    }

    private void Shoot(bool isActive) {
      var emissionModule = projectileParticle.emission;
      emissionModule.enabled = isActive;
    }
    
}
