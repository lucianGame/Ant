using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item 
{

   public ItemObjects Base { get; set; }
   public int Quantity { get; set; }

    public Item(ItemObjects pBase, int quantity)
    {
        Base = pBase;
        Quantity = pBase.Quantity;
    }

}
