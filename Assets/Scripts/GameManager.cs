using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject _startingGameTransition;
    [SerializeField] public GameObject _endingGameTransition;
    
    void Start()
    {
        _startingGameTransition.SetActive(true);
        Invoke("DisableStartingGameTransition", 5f);
    }



    private void DisableStartingGameTransition()
    {
        _startingGameTransition.SetActive(false);
    }




    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
