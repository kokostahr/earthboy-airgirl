using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    public string loadNewScene;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player1") && collider.CompareTag("Player2"))
        {
            SceneManager.LoadScene(loadNewScene);
        }
    }
}
