using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemeyExplosionVFX;
    [SerializeField] GameObject hitVFX;

    // Make a stack of vfx
    GameObject parentGameobject;
    [SerializeField] int killScore = 15;
    [SerializeField] int enemyHealth = 10;

    ScoreBoard scoreBoard;
    private void Start() 
    {
        AddRigidbody();
        parentGameobject = GameObject.FindGameObjectWithTag("Spawn At Runtime");
        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }
 
    private void OnParticleCollision(GameObject other) 
    {

        EnemyHit();

        if(enemyHealth <= 0)
        {
            DestroyEnemy();
            IncreaseEnemyKillScore();
        }
        
    }

    private void EnemyHit()
    {
        enemyHealth--;
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameobject.transform;
    }

    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(enemeyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameobject.transform;
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
