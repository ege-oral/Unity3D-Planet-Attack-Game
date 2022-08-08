using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log($"{this.name} collied to {other.gameObject.name}");   
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"{this.name} triggered to {other.gameObject.name}");   
    }
}
