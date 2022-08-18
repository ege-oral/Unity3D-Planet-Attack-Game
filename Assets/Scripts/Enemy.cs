using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemeyExplosionVFX;

    // Make a stack of vfx
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;

    ScoreBoard scoreBoard;
    private void Start() 
    {
       scoreBoard = FindObjectOfType<ScoreBoard>();    
    }
 
    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
        DestroyEnemy();
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
}
