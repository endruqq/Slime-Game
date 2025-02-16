using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");

        
        if (other.gameObject.name.Contains("Box"))
        {
            Debug.Log("Recognized box");

           
            if (Vector3.Distance(other.transform.position, transform.position) < 1f)
            {
                Debug.Log("Destroyed");
                Destroy(gameObject);
            }
        }
    }
}
