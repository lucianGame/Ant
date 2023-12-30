using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhittleClav : MonoBehaviour
{
    public static Animator anim;

    public static float Health = 80;
    public float maxHealth = 80;
    public static float defense = 2;
    public static float attack = 10;
    public static float speed = 20;

    public EnemyHealth healthBar;

    public Player player;
    public GameObject BattleSystem;

    public static bool Confident;
    public static bool Ashamed;
    public static bool Perplexed;







    public void PoisonDamage()
    {
        Health = Health - 2;
        healthBar.UpdateHealthBar();
    }

    public void CrowdCheers()
    {
        anim.SetTrigger("Stronger");
        WhittleClav.Health = WhittleClav.Health + 5;
        WhittleClav.attack = WhittleClav.attack + 1;
        WhittleClav.defense = WhittleClav.defense - 3;
        healthBar.UpdateHealthBar();

    }
    

   

 
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
       // Confident = false;
    }

    // Update is called once per frame
   
    
}
