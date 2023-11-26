using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    public GameObject prompt;

    public string itemName = "Some Item"; //each item will have its own name
    public Texture itemPreview; //the icon that will show in the inventory

    public GameObject item;

    public int stat;


    private void OnTriggerEnter(Collider other) //when you enter the item's trigger
    {
           prompt.SetActive(true); //the "pick up" prompt appears

    }

    private void OnTriggerStay(Collider other) //if you are in the item's trigger
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.WeaponStat = stat;
            Destroy(gameObject); //destroys the item we picked up from the over world
            Debug.Log("You got " + itemName + Player.WeaponStat);
            prompt.SetActive(false); //the prompt goes away
        }
    }

    private void OnTriggerExit(Collider other) //when you walk away from the item
    {
             prompt.SetActive(false); //the prompt goes away

    }
}
