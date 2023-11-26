using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    [SerializeField] Enemies _base; //the base stats of the enemy
    [SerializeField] int level;
     
    public Enemy enemy { get; set; } //takes from the scriptable enemy objects i've created

    public void Setup()
    {
      // enemy = new Enemy(_base, level); 
    }



}
