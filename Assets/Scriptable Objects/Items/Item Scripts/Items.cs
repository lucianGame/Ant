using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public string ItemChoice;
    public BattleSystemManager battleSystem;




    public void Crumb()
    {
        battleSystem.ItemChoice = "Crumb";
        battleSystem.ItemSelection();
    }
    public void Poison()
    {
        battleSystem.ItemChoice = "Poison";
        battleSystem.ItemSelection();
    }

    public void Honey()
    {
        battleSystem.ItemChoice = "Honey";
        battleSystem.ItemSelection();
    }

}
