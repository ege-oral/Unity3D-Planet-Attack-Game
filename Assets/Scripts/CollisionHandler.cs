using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem impactVFX;
    [SerializeField] float loadDelay = 1f;

    private void OnTriggerEnter(Collider other) 
    {
        GetComponent<MeshRenderer>().enabled = false;
        impactVFX.Play();
        FindObjectOfType<Player>().isAlive = false;

        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene(currentSceneIndex);  
    }


}
