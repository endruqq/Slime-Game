using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialougeManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialougeText;
    public Animator boxAnimator;
    
    private Queue<string> sentences;
    
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        
    }

    void Start()
    {
        boxAnimator.SetBool("IsOpen", false);
        
    }

    private void StartDialougAnimation()
    {
        boxAnimator.SetBool("IsOpen", true);
        
        
        
    }

    private void StartDialougeDisplayMessage()
    {
        
        DisplayNextSentence();
    }
    public void StartDialouge(Dialouge dialouge)
    {
        Invoke("StartDialougAnimation", 2f);
        Invoke("StartDialougeDisplayMessage", 2.5f);
        nameText.text = dialouge.name;
        sentences.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }
        
      string sentence = sentences.Dequeue();
      StopAllCoroutines();
      StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialouge()
    {
        boxAnimator.SetBool("IsOpen", false);
    }



}
