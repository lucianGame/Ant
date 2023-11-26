using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new enemy")] //lets me create new enemies as scriptable objects through the menu

public class Enemies : ScriptableObject
{

    [SerializeField] GameObject prefab; //holds the data for each enemy so i can put multiple in the world

    [SerializeField] string name;

    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defense;


    //exposes the private "name" variable
    public string Name
    {
        get { return name; }
    }

     //exposes the HP variable
    public int Hp
    {
        get { return hp; }
    }

    //exposes attack variable
    public int Attack
    {
        get { return attack; }
    }

    //exposes defense variable
    public int Defense
    {
        get { return defense; }
    }

    public GameObject Prefab
    {
        get { return prefab; }
    }
}
