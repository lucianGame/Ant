using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

  //  public GameObject prompt;

    public string itemName = "Some Item"; //each item will have its own name
    public Texture itemPreview; //the icon that will show in the inventory

    public GameObject item;

    public int itemCount = 0;

    private void OnTriggerEnter(Collider other) //when you enter the item's trigger
    {
     //   prompt.SetActive(true); //the "pick up" prompt appears
              
    }

    private void OnTriggerStay(Collider other) //if you are in the item's trigger
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.WeaponStat = 10;
            Destroy(gameObject); //destroys the item we picked up from the over world
            itemCount++;
            Debug.Log("You have" + itemCount + itemName);
           // prompt.SetActive(false); //the prompt goes away
        }
    }

    private void OnTriggerExit(Collider other) //when you walk away from the item
    {
  //      prompt.SetActive(false); //the prompt goes away

    }

}
