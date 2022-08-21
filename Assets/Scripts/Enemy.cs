using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemeyExplosionVFX;
    [SerializeField] GameObject hitVFX;

    // Make a stack of vfx
    [SerializeField] Transform parent;
    [SerializeField] int killScore = 15;
    [SerializeField] int enemyHealth = 10;

    ScoreBoard scoreBoard;
    private void Start() 
    {
        AddRigidbody();

        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }
 
    private void OnParticleCollision(GameObject other) 
    {

        EnemyHit();
        print("helo");

        if(enemyHealth <= 0)
        {
            DestroyEnemy();
            IncreaseEnemyKillScore();
        }
        
    }

    private void EnemyHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        enemyHealth--;
    }

    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(enemeyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    private void IncreaseEnemyKillScore()
    {
        scoreBoard.IncreaseScore(killScore);
    }

    private void AddRigidbody()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
}
