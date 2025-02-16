using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum BoxMoves
{
    box1move,
    box2move,
    box3move,
    box4move,
    box5move

}
public class groundScript : MonoBehaviour
{
    public BoxMoves boxMoves;
    public float numberOfMovesPushableCanDo;
    public TextMeshProUGUI numberOfMovesText;
    private Rigidbody2D rbBox;
    private bool isMerging = false;
    private bool wasAdded = true;

    public void Start()
    {
        BoxMovesValue();
        rbBox = GetComponent<Rigidbody2D>();
    }

    private void UpdateMoveText()
    {
        if(numberOfMovesText != null)
        {
            numberOfMovesText.text = numberOfMovesPushableCanDo.ToString();
        }
    }

    public void BoxMovesValue()
    {
        if(numberOfMovesPushableCanDo == 0) { 
        switch (boxMoves)
        {
            case BoxMoves.box1move:
                numberOfMovesPushableCanDo = 1;
                
                break;
            case BoxMoves.box2move:
                numberOfMovesPushableCanDo = 2;
                
                break;
            case BoxMoves.box3move:
                numberOfMovesPushableCanDo = 3;
                
                break;
            case BoxMoves.box4move:
                numberOfMovesPushableCanDo = 4;
                
                break;
            case BoxMoves.box5move:
                numberOfMovesPushableCanDo = 5;
                
                break;
        }
        }

        numberOfMovesText.text = numberOfMovesPushableCanDo.ToString();
    }

    public void DecreaseMoves()
    {
        if(numberOfMovesPushableCanDo > 0)
        {
            numberOfMovesPushableCanDo--;
            numberOfMovesText.text = numberOfMovesPushableCanDo.ToString();
        }
        if(numberOfMovesPushableCanDo <= 0)
        {
 
                ChangeToNonPushableLayer();
           
            
       
        }


    }

    private void ChangeToNonPushableLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Obstacle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMerging) return;
        

        if (collision.gameObject.CompareTag("Pushable"))
        {

            groundScript otherBox = collision.gameObject.GetComponent<groundScript>();
            if (otherBox != null && !otherBox.isMerging)
            {
                MergeBoxes(otherBox);

            }
        }
        

    }

    public void MergeBoxes(groundScript otherBox)
    {
        isMerging = true;
        otherBox.isMerging = true;

        Debug.Log("Merging: This box moves = " + this.numberOfMovesPushableCanDo + ", Other box moves = " + otherBox.numberOfMovesPushableCanDo);

        this.numberOfMovesPushableCanDo += otherBox.numberOfMovesPushableCanDo;
        this.numberOfMovesPushableCanDo++;
        UpdateMoveText();
        if(numberOfMovesPushableCanDo > 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
       


        Destroy(otherBox.gameObject);

        isMerging = false;
        



    }


}
