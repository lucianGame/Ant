using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldDialogue : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] int fastLetters;

    [SerializeField] Text dialogueText;
    [SerializeField] GameObject dialogueBox;

    public AK.Wwise.Event talking;



    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;
    bool isTalking;


    [SerializeField] PlayerMovement playerMovement;



    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
      
        playerMovement.enabled = false;
        yield return new WaitForEndOfFrame();

        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[currentLine])); //play dialogue starting on line 0

        
    }


    public IEnumerator TypeDialogue(string line) //types dialogue out one line at a time
    {
        isTyping = true;
        dialogueText.text = ""; //start out with a blank dialogue box
        foreach (var letter in line.ToCharArray()) //play the loop once for each letter in the dialogue
        {
            dialogueText.text += letter; //adds one letter to the text box
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                talking.Post(gameObject);
                yield return new WaitForSeconds(1f / fastLetters); //text goes faster
            }
            else
            {
                talking.Post(gameObject);
                yield return new WaitForSeconds(1f / lettersPerSecond); //controls how many letters per second appear
            }

            isTyping = false;
        }

        yield return new WaitForSeconds(1);
        NextLine();

    }


    public void NextLine() //go to the next line of dialogue
    {
        if (Input.GetKeyDown(KeyCode.Space) || !isTyping) //will only go to next line is the current line is done typing
        {
            ++currentLine; //goes to the next line of dialogue
            if(currentLine < dialogue.Lines.Count) //if there are still lines to be played
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine])); //plays the next line
            }
            else
            {
                playerMovement.enabled = true;
                currentLine = 0; //reset the dialogue upon closing it
                dialogueBox.SetActive(false);
                NPCController.isTalking = false;
            }
        }
    }

 


}
