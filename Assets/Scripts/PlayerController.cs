using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPosistion;
    private float timeToMove = 0.1f;
    public float numberOfMovesPlayerCanDo;
    public bool canMove = true;
    public GameObject boxColliderPrefab;
    public float raycastDistance = 1f;
    public LayerMask obstacleLayer;
    public LayerMask boxLayer;
    public float numberOfMovesPushableObjectHas;
    public TextMeshProUGUI playerMovesText;
    public groundScript groundScript;
    private AudioSource audioSource;
    public AudioClip moveSound;
    public AudioClip moveBoxSound;

    private Rigidbody2D rb;  

    public void PlaySound(AudioClip clip)
    {
        if(audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }


    void Start()
    {
       
        numberOfMovesPlayerCanDo = 17f;
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D attached to the player
        rb.isKinematic = true; // Set it to Kinematic to manually control movement
        numberOfMovesPushableObjectHas = 5;
        
        GameObject pushableObject = GameObject.FindGameObjectWithTag("Pushable");
        if (pushableObject != null)
        {
            groundScript = GameObject.FindGameObjectWithTag("Pushable").GetComponent<groundScript>();// object reference not set// repair it
        }
        
        
        
        playerMovesText.text = numberOfMovesPlayerCanDo.ToString();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        bool canMove = numberOfMovesPlayerCanDo > 0;
        if (Input.GetKeyDown(KeyCode.D) && !isMoving && canMove && CanMove(Vector2.right))
        {
            StartCoroutine(MoveDirections(Vector3.right));
            PlayerMoveSubMoves();
            PlaySound(moveSound);
            
        }

        if (Input.GetKeyDown(KeyCode.A) && !isMoving && canMove && CanMove(Vector2.left))
        {
            StartCoroutine(MoveDirections(Vector3.left));
            PlayerMoveSubMoves();
            PlaySound(moveSound);
        }

        if (Input.GetKeyDown(KeyCode.W) && !isMoving && canMove && CanMove(Vector2.up))
        {
            StartCoroutine(MoveDirections(Vector3.up));
            PlayerMoveSubMoves();
            PlaySound(moveSound);
        }

        if (Input.GetKeyDown(KeyCode.S) && !isMoving && canMove && CanMove(Vector2.down))
        {
            StartCoroutine(MoveDirections(Vector3.down));
            PlayerMoveSubMoves();
            PlaySound(moveSound);
        }
    }

    void PlayerMoveSubMoves()
    {
        numberOfMovesPlayerCanDo--;
        if (numberOfMovesPlayerCanDo == 0)
        {
            canMove = false;
        }
        playerMovesText.text = numberOfMovesPlayerCanDo.ToString();
    }

    bool CanMove(Vector2 direction)
    {
        Debug.DrawRay(rb.position, direction * raycastDistance, Color.red, 0.5f);
        // Perform a raycast in the specified direction from the player's position
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, raycastDistance, obstacleLayer);

        // Check if the raycast hit anything
        if (hit.collider != null)
        {
            Debug.Log("Blocked by: " + hit.collider.gameObject.name);
            return false;  // There's an obstacle, so the player can't move in this direction
        }

        RaycastHit2D checkForBox = Physics2D.Raycast(rb.position, direction, raycastDistance, boxLayer);

        if(checkForBox.collider != null)
        {
            if (checkForBox.collider.CompareTag("Pushable") )
            {
                groundScript boxScript = checkForBox.collider.GetComponent<groundScript>();
                if (boxScript != null && boxScript.numberOfMovesPushableCanDo > 0)
                {
                    PushObject(checkForBox.collider.gameObject, direction, boxScript);
                    return true;
                }
            }
            else
            {
                Debug.Log("Blocked by: " + hit.collider.gameObject.name);
                return false;  // There's an obstacle, so the player can't move
            }
        }

        return true;  // No obstacle, the player can move
    }

    void PushObject(GameObject obj, Vector2 direction,groundScript boxScript )
    {
        Rigidbody2D objectRb = obj.GetComponent<Rigidbody2D>();

        if (objectRb != null)
        {
            
            objectRb.MovePosition(objectRb.position + direction * raycastDistance);
            PlaySound(moveBoxSound);
            boxScript.DecreaseMoves();
            
        }
    }
    private IEnumerator MoveDirections(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = rb.position;  // Use Rigidbody2D position instead of transform.position
        targetPosistion = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            Vector3 newPos = Vector3.Lerp(origPos, targetPosistion, (elapsedTime / timeToMove));
            rb.MovePosition(newPos);  // Move using Rigidbody2D to ensure proper collision detection
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosistion);  // Ensure the final position is set correctly
        isMoving = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IceGround"))
        {
            Destroy(collision.gameObject);
        
       

        Vector3 colliderPositinOffSet = new Vector3(0.28f, 0.30f, 0);
        // Optionally replace with BoxCollider to block the area after the square is destroyed
        GameObject newCollider = Instantiate(boxColliderPrefab, collision.transform.position + colliderPositinOffSet, Quaternion.identity);
        newCollider.GetComponent<BoxCollider2D>().isTrigger = false;  // Ensure the new collider is not a trigger
        }
    }
}
