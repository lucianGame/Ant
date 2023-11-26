using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using UnityEngine.SceneManagement;

public class BattleSystemManager : MonoBehaviour
{

    public Player player;
    public WhittleClav enemy;

    public BattleDialogue textBox;

    public Button attack;

    private BattleState battleState;


    public enum BattleState { Begin, PlayerTurn, EnemyTurn, Intermission, Win, Lost };

    public static bool enemyPoisoned;
    public static bool enemyStunned;

    public static bool playerPoisoned;
    public static bool playerDefended;
    public static bool countered;

    public HealthBar healthBar;
    public EnemyHealth enemyHealthBar;
    public SpecialBar specialBar;
    public GameObject winText;

  

    public int poisonTurns = 3;

    public int playPoisonTurns = 3;
    public int defenseTurns = 1;

    public string ItemChoice;

    public int crumbNum;
    public int poisonNum;

    bool enemyCharging = false;

    public static string character; //the character that is talking (to change dialogue sound effects)

   

    [Header("Audio")]

    public AK.Wwise.Event talking;
    public AK.Wwise.Event slash;
    public AK.Wwise.Event superSlash;
    public AK.Wwise.Event eating;
    public AK.Wwise.Event drinking;
    public AK.Wwise.Event bite;
    public AK.Wwise.Event charging;
    public AK.Wwise.Event punch;
    public AK.Wwise.Event superPunch;
    public AK.Wwise.Event defend;
    public AK.Wwise.Event counter;
    public AK.Wwise.Event scroll;
    public AK.Wwise.Event select;
    public AK.Wwise.Event whittleHit;




    private void Start()
    {
       StartCoroutine(Setup());
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator Setup()
    {
        textBox.SetItems(player.inventory.items);

        character = "whittle"; //whittle clav is talking
        yield return StartCoroutine(textBox.TypeDialogue("You have dishonored my family name! We will fight."));
        yield return new WaitForSeconds(1); //wait until the dialogue is through to go to the player turn

        PlayerTurn();
    }

    public void Update()
    {
        if (poisonTurns < 1) // Whittle's poisonTurns starts at 3 and decreases one every turn
        {
            enemyPoisoned = false; //When poisonTurns reaches zero, Whittle stops taking poison damage
        } //end if

        if (playPoisonTurns < 1) //This is the same thing as above but with the player's poisonTurns
        {
            playerPoisoned = false;
        } //end if

        if(battleState == BattleState.PlayerTurn) //playing the scroll and select SFX
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                scroll.Post(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.Space)){
                select.Post(gameObject);
            }
        }
    }





    public void PlayerTurn() //The player begins their turn
    {

        battleState = BattleState.PlayerTurn;
        Player.anim.SetInteger("Default", 0); //Default fighting animation
        textBox.EnableActionSelector(true); //The attack, defend, and item buttons are visible

        attack.Select(); //The attack button is highlighted at the start of the player turn
        textBox.EnableTextBox(false); //There is no dialogue during this stage
        textBox.EnableAttackSelector(false);  //attack options are not visible
        textBox.EnableItemSelector(false); //items are not visible

        


        if (playerDefended) //if the player defended on the last turn
        {
            playerDefended = false; //the player is no longer defending
            player.defense = player.defense - 3; //defence stat goes back to its original state
        } //end if

        if (player.bp < 3) //If the player does not have enough BP for this attack the button grays out
        {
            textBox.specialAttack.interactable = false;
        }
        else
        {
            textBox.specialAttack.interactable = true;
        } //end if
        
    }

    public void EnemyTurn() //Whittle Clav's turn begins
    {
        battleState = BattleState.EnemyTurn;
       
        StartCoroutine(EnemyAttack()); //Whittle Clav attacks
    }

    public void AttackHover() //Called upon attack button hover
    {
        textBox.EnableAttackSelector(true); //shows the attack options
        textBox.EnableItemSelector(false);
        

    }
    public void DefendHover() //Called when the player is hovering over the defend button
    {
        textBox.EnableItemSelector(false);
        textBox.EnableAttackSelector(false);
    }

    public void ItemsHover() //Called when the player is hovering over the item button
    {
        textBox.EnableItemSelector(true); //shows the item options
        textBox.EnableAttackSelector(false);
    
    }

    public void Scroll()
    {
        scroll.Post(gameObject);
    }

    public void CrumbHover()
    {
        //crumbDesc.SetActive(true);
       // honeyDesc.SetActive(false);
       // poisonDesc.SetActive(false);
    }

    public void HoneyHover()
    {
       // honeyDesc.SetActive(true);
        //poisonDesc.SetActive(false);
        //crumbDesc.SetActive(false);
    }

    public void PoisonHover()
    {
      //  poisonDesc.SetActive(true);
       // honeyDesc.SetActive(false);
       // crumbDesc.SetActive(false);
       
    }
    

    public void SlashStart() //Called when the player clicks the Slash button
    {
        StartCoroutine(Slash()); //Slash attack begins
    }
    public void SuperSlashStart() //Called when the player clicks the Super Slash button
    {
        StartCoroutine(SuperSlash());
    }

    public void ItemSelection() //Called when the player clicks an item button
    {
        StartCoroutine(Item());
    }

    public void DefendStart() //Called when the player clicks the defend button
    {
        StartCoroutine(Defend());
    }





    IEnumerator Slash()
    {

        Player.anim.SetTrigger("Slash"); //Player's attack animation plays
        yield return new WaitForSeconds(0.2f); //aligns the sound with the animation
        slash.Post(gameObject);
        textBox.EnableActionSelector(false);
        WhittleClav.anim.SetTrigger("Hit"); //Whittle Clav's hit animation plays
        charging.Stop(gameObject);
        whittleHit.Post(gameObject); //play whittle clav's hit SFX
        yield return new WaitForSeconds(1); //Gives the animation time to play
        if (enemyCharging == true)
        {
            charging.Post(gameObject);
        }
        WhittleClav.Health = WhittleClav.Health - (Player.WeaponStat - WhittleClav.defense); //Whittle Clav takes damage based on his defence the player's strength
        enemyHealthBar.UpdateHealthBar(); //The health bar goes down to reflect the damage Whittle took


        if (WhittleClav.Health <= 0) //If Whittle Clav's health is depleated
        {
            battleState = BattleState.Win; //The player wins, winning sequence begins
            StartCoroutine(BattleWon());
        }
        else //If Whittle Clav is still alive
        {

            EnemyTurn(); //whittle clav's turn
        } //end if

    }



    IEnumerator SuperSlash()
    {
        Player.anim.SetTrigger("Special Attack"); //Player's attack animation plays
        yield return new WaitForSeconds(0.5f); //aligns the sound with the animation
        superSlash.Post(gameObject);
        textBox.EnableActionSelector(false);
        player.bp = player.bp -3; //3 mana points are used for this attack
        Debug.Log(player.bp);
        specialBar.UpdateSpecialBar();
        yield return new WaitForSeconds(1); //Gives the animation time to play
        charging.Stop(gameObject);
        WhittleClav.anim.SetTrigger("Hit");
        whittleHit.Post(gameObject); //play whittle clav's hit SFX
        yield return new WaitForSeconds(1); //Gives the animation time to play
        if (enemyCharging == true)
        {
            charging.Post(gameObject);
        }
        WhittleClav.Health = WhittleClav.Health - (Player.WeaponStat - WhittleClav.defense) * 2; //whittle clav takes twice as much damage as usual
        enemyHealthBar.UpdateHealthBar();

        if (WhittleClav.Health <= 0) //if enemy is dead
        {
            battleState = BattleState.Win;
            StartCoroutine(BattleWon());
        }
        else
        {

            EnemyTurn(); //whittle clav's turn
        } //end if


    }



    IEnumerator Item()
    {
        textBox.EnableActionSelector(false);



        switch (ItemChoice) //String is selected based on which item the player clicks on
        {
            case "Crumb":

                Player.anim.SetTrigger("Eat"); //The player has an eating animation
                yield return new WaitForSeconds(0.15f); //aligns the sound with the animation
                eating.Post(gameObject);
                yield return new WaitForSeconds(3); //Gives the animation time to play
                player.Health = player.Health + 5; //The player restores 5 HP
                healthBar.UpdateHealthBar(); //Health bar goes up to reflect HP restored
              
              
                textBox.EnableTextBox(true); //the textbox is visible
                character = "narrator";
                yield return StartCoroutine(textBox.TypeDialogue("Five HP restored!")); //plays the dialogue
                yield return new WaitForSeconds(1); //wait until the dialogue is through
                crumbNum -= 1; //The player has one less crumb
                break;

            case "Poison":

                enemyPoisoned = true; //Whittle Clav is poisoned and will take damage for 3 turns

                textBox.EnableTextBox(true); //the textbox is visible
                character = "narrator";
                yield return StartCoroutine(textBox.TypeDialogue("Whittle Clav has been poisoned!")); //plays the dialogue
                yield return new WaitForSeconds(1); //wait until the dialogue is through
                poisonNum -= 1; //The player has one less poison
             
                break;

            case "Honey":
                Player.anim.SetTrigger("Eat"); //The player has an eating animation
                yield return new WaitForSeconds(0.15f); //aligns the sound with the animation
                eating.Post(gameObject);
                yield return new WaitForSeconds(3); //Gives the animation time to play
                textBox.EnableTextBox(true); //the textbox is visible
                character = "narrator";
                yield return StartCoroutine(textBox.TypeDialogue("Five BP restored!")); //plays the dialogue
                player.bp = player.bp + 5; //The player restores 5 bp
                specialBar.UpdateSpecialBar(); //Special bar is updated to reflect bp restored
                break;



            default: break;
        } //end switch

        if (WhittleClav.Health <= 0) //If Whittle Clav has no more HP
        {
            battleState = BattleState.Win;
            StartCoroutine(BattleWon()); //Winning sequence begins
        }
        else //If Whittle Clav is still alive and kicking
        {

            EnemyTurn(); //whittle clav's turn
        } //end if


    }


    IEnumerator Defend()
    {
        textBox.EnableActionSelector(false);
        playerDefended = true; //The player's defence goes up by 3 and they can counter
        Player.anim.SetInteger("Default", 1); //Defend animation plays on a loop
        yield return new WaitForSeconds(1);
        EnemyTurn(); //whittle clav's turn
    }


    IEnumerator EnemyAttack() //WHITTLE CLAV'S TURN
    {
        textBox.EnableActionSelector(false);
        textBox.EnableTextBox(false);
        yield return new WaitForSeconds(1);

        if (playerDefended) //If the player defended on the last turn
        {
            player.defense = player.defense + 3; //Player's defence stat increases by 3 for one turn
        } //end if



        if (playerPoisoned) //If Whittle Clav bit the player on one of the 3 previous turn and they are poisoned
        {

            player.Health -= 2; //The player takes 2 damage
            healthBar.UpdateHealthBar(); //Health bar is updated
            playPoisonTurns -= 1; //The player's poison turns goes down by one

        } //end if

        if (enemyCharging) //If Whittle began charging on previous turn
        {
            charging.Stop(gameObject); //the charging sfx stops
            WhittleClav.anim.SetTrigger("Punch"); //Whittle Clav unleashes his super punch
            yield return new WaitForSeconds(0.3f);

            superPunch.Post(gameObject); //play the super punch sfx

            Debug.Log("Super Punch!");
            yield return new WaitForSeconds(1);

            if (countered)  //if you press space at the exact right time while ur enemy is attacking
            {
                WhittleClav.Health = WhittleClav.Health - WhittleClav.attack;
                enemyHealthBar.UpdateHealthBar();
                WhittleClav.anim.SetTrigger("Hit");
                countered = false;
                player.bp += 5;
                specialBar.UpdateSpecialBar();
            }
            else
            {
                Player.anim.SetTrigger("Hit"); //Player has hit animation
                player.Health -= (WhittleClav.attack - player.defense) * 2; //Player takes twice as much damage as a normal punch
                healthBar.UpdateHealthBar(); //Health bar is updated
            }
            

            WhittleClav.anim.SetInteger("Default", 0); //Whittle Clav goes back to hs default animation
          
            enemyCharging = false;  //Whittle Clav is no longer charging
        }
        else //If Whittle Clav is not charging
        {
            float randAttack = UnityEngine.Random.Range(0, 4); //Whittle Clav's attack is chosen by random
            switch (randAttack)
            {
                case 0: //Whittle Clav's default attack
                    WhittleClav.anim.SetTrigger("Punch"); //Punch animation plays

                    yield return new WaitForSeconds(0.3f);
                    punch.Post(gameObject); //play the sound effect

                    yield return new WaitForSeconds(1);

                    if (countered)
                    {
                        WhittleClav.Health = WhittleClav.Health - WhittleClav.attack;
                        enemyHealthBar.UpdateHealthBar();
                        WhittleClav.anim.SetTrigger("Hit");
                        countered = false;
                        player.bp += 5;
                        specialBar.UpdateSpecialBar();
                    }
                      else 
                    {
                        Player.anim.SetTrigger("Hit"); //Hit animation plays
                        player.Health -= (WhittleClav.attack - player.defense); //Player's health goes down
                        healthBar.UpdateHealthBar(); //Health bar is updated
                    }
                    break;
                case 1: //Whittle Clav's default attack again
                    WhittleClav.anim.SetTrigger("Punch");

                    yield return new WaitForSeconds(0.3f);
                    punch.Post(gameObject); //play the sound effect

                    yield return new WaitForSeconds(1);

                    if (countered)
                    {
                        WhittleClav.Health = WhittleClav.Health - WhittleClav.attack;
                        enemyHealthBar.UpdateHealthBar();
                        WhittleClav.anim.SetTrigger("Hit");
                        countered = false;
                        player.bp += 5;
                        specialBar.UpdateSpecialBar();
                    }
                    else 
                    {
                        Player.anim.SetTrigger("Hit"); //Hit animation plays
                        player.Health = player.Health - (WhittleClav.attack - player.defense);
                        healthBar.UpdateHealthBar();
                    }

                    break;

                case 2: //Whittle Clav charges up for his super punch
                    enemyCharging = true; // whittle clav is charging his attack
                    WhittleClav.anim.SetInteger("Default", 1); // plays whittle clav's charge animation on loop until next turn
                    charging.Post(gameObject);
                    textBox.EnableTextBox(true);
                    character = "narrator";
                    yield return StartCoroutine(textBox.TypeDialogue("Whittle Clav is charging his attack."));
                    yield return new WaitForSeconds(1);

                    break;

                case 3: //Whittle Clav bites the player, poisoning them
                    WhittleClav.anim.SetTrigger("Bite"); //Bite animation plays
                    bite.Post(gameObject); // play the sound effect
                    yield return new WaitForSeconds(1); //Gives animation time to play
                    Player.anim.SetTrigger("Hit"); //Hit animation plays
                   
                    yield return new WaitForSeconds(1);

                    player.Health -= (WhittleClav.attack - player.defense); //player takes damage
                    playerPoisoned = true; //player is now poisoned
                    playPoisonTurns = 3; //This stat goes down 1 every turn. Once it reaches 0 the player is no longer poisoned
                    healthBar.UpdateHealthBar(); //Health bar is updated

                    textBox.EnableTextBox(true); //the textbox is visible
                    character = "narrator";
                    yield return StartCoroutine(textBox.TypeDialogue("You have been poisoned!")); //plays the dialogue
                    yield return new WaitForSeconds(1); //wait until the dialogue is through

                    break;

                default:
                    break;
            }
        } //end if




        if (enemyPoisoned) // If the player used the poison item on Whittle
        {
            poisonTurns -= 1;  //poison turns goes down by one, once it reaches zero whittle clav is no longer poisoned
            WhittleClav.anim.SetTrigger("Hit");
            yield return new WaitForSeconds(1);
            WhittleClav.Health = WhittleClav.Health - 2; //whittle clav takes 2 points of damage
            enemyHealthBar.UpdateHealthBar();
            Debug.Log("Poison Damage");

        } //end if

        if (player.Health <= 0) //if player is dead
        {
            battleState = BattleState.Lost;
            StartCoroutine(BattleLost());
        }
        else if (WhittleClav.Health <= 0) //if enemy is dead
        {
            battleState = BattleState.Win;
            StartCoroutine(BattleWon());
        }
        else //if everyone is still alive
        {

            PlayerTurn();

        } //end if
    }

    public void Intermission()
    {

        Player.anim.SetInteger("Default", 0);


    }

    IEnumerator BattleWon() //if whittle clav runs out of HP
    {
        textBox.EnableActionSelector(false);
        textBox.EnableTextBox(true); //there is dialogue
        character = "whittle";
        yield return StartCoroutine(textBox.TypeDialogue("You have bested me?! How?!?! Hssss...."));
        yield return new WaitForSeconds(1);
        textBox.EnableTextBox(false);
        winText.SetActive(true); //displays the winner text
        WhittleClav.anim.SetTrigger("Lost"); //whittle clav has a sad losing animation
        Player.anim.SetTrigger("Victory"); //player does their victory dance
        yield return new WaitForSeconds(2);
      //  SceneManager.LoadScene(0); //takes player back to the tunnel scene
    }

    IEnumerator BattleLost() //if player runs out of HP
    {
        textBox.EnableActionSelector(false);
        textBox.EnableTextBox(true);
        character = "whittle";
        yield return StartCoroutine(textBox.TypeDialogue("Ha ha ha! The champion Whittle Clav never loses!"));
        yield return new WaitForSeconds(1);
       // loseText.SetActive(true);
        WhittleClav.anim.SetTrigger("Victory");
        Player.anim.SetTrigger("Lost");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0); //takes player back to the tunnel scene

    }

}
