using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");

        // Check if the collided object is named "Box" or contains "Box"
        if (other.gameObject.name.Contains("Box"))
        {
            Debug.Log("Recognized box");

            // Compare positions using Vector3.Distance for more reliable comparison
            if (Vector3.Distance(other.transform.position, transform.position) < 1f)
            {
                Debug.Log("Destroyed");
                Destroy(gameObject);
            }
        }
    }
}
