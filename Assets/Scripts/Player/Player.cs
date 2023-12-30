using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Inventory inventory; //gives the player an item inventory

    public static Animator anim;
    private CharacterController controller;

    public float Health;
    public float maxHealth;
    public float defense;
    public float maxBP;
    public float bp;
    public static float WeaponStat = 7;

    public static int weaponEquiped;


    public BattleSystemManager battleSystem;


    private void Awake()
    {
        inventory = new Inventory();
    }

    // Start is called before the first frame update
    void Start()
    { 


        anim = gameObject.GetComponentInChildren<Animator>();

        controller = GetComponent<CharacterController>();

    }

    public void OnTriggerEnter(Collider other)
    {
       // int item = other.GetComponent<Items>();

    }



}
