using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem impactVFX;
    [SerializeField] float loadDelay = 1f;
    [SerializeField] List<GameObject> childCollidersObjects;

    [SerializeField] GameObject finishPoint;

    private void Start() 
    {
        Invoke("ActivateFinishCollider", 3f);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Finish")
        {
            StartCoroutine(Finish());
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            DeactivateAllChildColliders();
            impactVFX.Play();
            FindObjectOfType<Player>().isAlive = false;
            StartCoroutine(ReloadLevel());
        }
        
    }

    IEnumerator ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene(currentSceneIndex);  
    }

    private void DeactivateAllChildColliders()
    {
        foreach(GameObject go in childCollidersObjects)
        {
            go.SetActive(false);
        }
    }

    private void ActivateFinishCollider()
    {
        finishPoint.GetComponent<BoxCollider>().enabled = true;
    } 

    IEnumerator Finish()
    {
        // If player reaches the end.
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(activeSceneIndex + 1); 
    }
}
