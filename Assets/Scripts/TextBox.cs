using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{

    public GameObject container_NPC;
    public GameObject container_Player;
    public GameObject label;
    public Text text_NPC;
    public Text[] text_Choices;
    public Text NPC_label;


    public static bool Talking;
    public static bool Intermission;
    public static bool playerPoisoned;
    public static bool enemyPoisoned;

    bool animatingText = false;

    string NextDialogue;

    IEnumerator NPC_TextAnimator;


    void Start()
    {
        container_NPC.SetActive(false);
        container_Player.SetActive(false);

    }

    void Update()
    {

        if (Talking)
        {
            if (!VD.isActive) //If the dialogue has not started yet
            {

                if (Intermission) //Random events that can happen after enemy turn before player turn
                {

                    float random = UnityEngine.Random.Range(0, 5);
                    switch (random)
                    {
                        case 0: //Nothing happens, the player's turn begins

                            Debug.Log("The crowd watches closely");
                            GetComponent<VIDE_Assign>().overrideStartNode = 13;


                            break;

                        case 1: //The crowd cheers for Whittle Clav, raising his confidence
                            Debug.Log("The fans cheer");
                            GetComponent<VIDE_Assign>().overrideStartNode = 6;

                            break;

                        case 2: //Someone throws a tomato at the player.
                            Debug.Log("Tomato");
                            GetComponent<VIDE_Assign>().overrideStartNode = 10;

                            break;

                        case 3: //Nothing happens, the player's turn begins

                            Debug.Log("The crowd watches closely");
                            GetComponent<VIDE_Assign>().overrideStartNode = 13;


                            break;

                        case 4: //Nothing happens, the player's turn begins

                            Debug.Log("The crowd watches closely");
                            GetComponent<VIDE_Assign>().overrideStartNode = 13;


                            break;

                        default: break;


                    }

                    Intermission = false;
                }
                else if (playerPoisoned)
                {
                    GetComponent<VIDE_Assign>().overrideStartNode = 8;
                   // playerPoisoned = false;
                } //end if
                
                Begin();
            }

            else if (Input.GetKeyDown(KeyCode.Space))
            {

                VD.Next(); //Goes to the next dialogue box
            }
        }
    }

    void Begin()
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }


    void UpdateUI(VD.NodeData data)
    {
        container_NPC.SetActive(false);
        container_Player.SetActive(false);

        if (Talking)
        {

            container_NPC.SetActive(true);
            text_NPC.text = data.comments[data.commentIndex];

            //This coroutine animates the NPC text instead of displaying it all at once
            //	NPC_TextAnimator = DrawText(data.comments[data.commentIndex], 0.02f);
            //StartCoroutine(NPC_TextAnimator);

            if (data.tag.Length > 0)
            {
                NPC_label.text = data.tag;
                label.SetActive(true);
            }
            else
            {

                NPC_label.text = VD.assigned.alias;

                label.SetActive(false);
            }
        }
    }

    void End(VD.NodeData data)
    {
        container_NPC.SetActive(false);
        container_Player.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();

    }

    void OnDisable()
    {
        if (container_NPC != null)
            End(null);
    }

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        if (Input.GetMouseButtonUp(0))
            VD.Next();
    }
}

//	IEnumerator DrawText(string text, float time)
//	{
//	animatingText = true;
//
//string[] words = text.Split(' ');

//		for (int i = 0; i < words.Length; i++)
//	{
//	string word = words[i];
//	if (i != words.Length - 1) word += " ";

//	string previousText = text_NPC.text;

//	float lastHeight = text_NPC.preferredHeight;
//	text_NPC.text += word;
//	if (text_NPC.preferredHeight > lastHeight)
//	{
//		previousText += System.Environment.NewLine;
//	}

//	for (int j = 0; j < word.Length; j++)
//	{
//		text_NPC.text = previousText + word.Substring(0, j + 1);
//		yield return new WaitForSeconds(time);
//	}
//	}
//text_NPC.text = text;
//	animatingText = false;
//	}
//}
