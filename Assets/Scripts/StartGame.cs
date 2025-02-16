using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameManager gameManager; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager._endingGameTransition.SetActive(true);
            Invoke("LoadNextScene", 1.5f);
            
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Lv1Tutorial");
    }


}
