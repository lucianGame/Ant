using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{


    public int lettersPerSecond;
    public int fastLetters;

    public Text dialogueText;
    public GameObject dialogueBox;
    public GameObject actionSelector; 
    public GameObject attackSelector; 
    public GameObject itemSelector;

    public AK.Wwise.Event talk;




    public Button specialAttack;

    public List<Button> actionChoices; //choose between ATTACK, DEFEND, and ITEMS
    public List<Button> attackChoices; //choose between slash and super slash, more to be added later
   // public List<Button> itemChoices; //choose between crumb, honey, or poison, more to be added later

    public List<Text> itemChoices; //choose between crumb, honey, or poison, more to be added later

    public Text itemDetails; //the description of the item being hovered over




    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue) //types the dialogue out one letter at a time
    {
        dialogueText.text = ""; //start out with a blank dialogue box
        foreach(var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter; //adds one letter to the text box
            
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                yield return new WaitForSeconds(1f / fastLetters); //text goes faster
            }
            else
            {
                yield return new WaitForSeconds(1f / lettersPerSecond); //controls how many letters per second appear
                if (BattleSystemManager.character == "whittle")
                {
                    talk.Post(gameObject);
                }
            }
        }
    }

    public void EnableTextBox(bool enabled)
    {
        dialogueBox.SetActive(enabled); //the dialogue is playing on start
      
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
       
    }

    public void EnableAttackSelector(bool enabled)
    {
        attackSelector.SetActive(enabled);

    }

    public void EnableItemSelector(bool enabled)
    {
        itemSelector.SetActive(enabled);
        //itemDetails.SetActive(enabled);

    }

   

    public void SetItems(List<Item> items)
    {
        for(int i = 0; i < itemChoices.Count; i++)
        {
            if(i < items.Count)
            {
                itemChoices[i].text = items[i].Base.Name;
            }
        }
    }


}
