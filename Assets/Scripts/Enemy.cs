using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemeyExplosionVFX;

    // Make a stack of vfx
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;
    int enemyHealth = 10;

    ScoreBoard scoreBoard;
    private void Start() 
    {
       scoreBoard = FindObjectOfType<ScoreBoard>();    
    }
 
    private void OnParticleCollision(GameObject other) 
    {
        enemyHealth--;
        if(enemyHealth <= 0)
        {
            ProcessHit();
            DestroyEnemy();
        }
        
    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }
    
    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(enemeyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    private void Hit()
    {
        
    }
}
