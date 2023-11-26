using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Items", menuName = "Items/Create new Item")] //allows me to create new items as assets

public enum ItemType
{
    Food,
    Equipment,
    Defualt
}

public abstract class ItemObjects : ScriptableObject
{


    [SerializeField] GameObject prefab; //holds the data for each item so i can put multiple in the world
    public ItemType type; //hold the type of item

    [SerializeField] string name; //holds the item's name

    [TextArea(15, 20)]
    [SerializeField] string description; //description of the item

    //Only for items that restore bp or hp
    [SerializeField] int hpRestored;
    [SerializeField] int bpRestored;

    [SerializeField] int quantity; //number of items the player has

    //returns the data of the items
    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public int HPRestored
    {
        get { return hpRestored; }
    }
    public int BPRestored
    {
        get { return bpRestored; }
    }

    public int Quantity
    {
        get { return quantity; }
    }
}
