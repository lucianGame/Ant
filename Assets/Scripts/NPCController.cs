using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

   public OverworldDialogue textBox;
   [SerializeField] Dialogue dialogue;

    public static bool isTalking = false;

   // public AK.Wwise.Event Talking;

    public GameObject prompt;


    public void OnTriggerEnter(Collider other) //prompt is shown upon entering the trigger
    {
        prompt.SetActive(true);

    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTalking) //only start dialogue if dialogue is not already running
        {

            Interact();
            isTalking = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        prompt.SetActive(false);
    }


    public void Interact()
    {
        prompt.SetActive(false);
        StartCoroutine(textBox.ShowDialogue(dialogue));
    }
}
