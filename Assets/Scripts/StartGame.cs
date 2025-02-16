using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameManager gameManager; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
