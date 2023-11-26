using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public List<Item> items { get; set; }

    public Inventory()
    {
        items = new List<Item>();
        

      //  AddItem(new Item { itemType = Item.ItemType.BoxingGloves, quantity = 1 });
        Debug.Log(items.Count);

        Debug.Log("Inventory");
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

}



