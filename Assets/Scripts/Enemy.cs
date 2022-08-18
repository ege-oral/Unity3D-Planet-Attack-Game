using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemeyExplosionVFX;

    // Make a stack of vfx
    [SerializeField] Transform parent;
 
    private void OnParticleCollision(GameObject other) 
    {
        GameObject vfx = Instantiate(enemeyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }


}
