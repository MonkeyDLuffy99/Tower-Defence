using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlesPrefab;
    [SerializeField] ParticleSystem deathParticlesPrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnParticleCollision(GameObject other) {
            ProcessHit();
            if (hitPoints <= 0) {
                KillEnemy();
            }
    }

    void ProcessHit() {
        hitPoints = hitPoints - 1;
        var hitFx = Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
        hitFx.Play();
        Destroy(hitFx.gameObject, hitFx.main.duration);
        myAudioSource.PlayOneShot(enemyHitSFX);
    }

    void KillEnemy() {
        var vfx = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        Destroy(transform.parent.gameObject);
    }
}
