using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhittleTrigger : MonoBehaviour
{

	public bool inTrigger;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			inTrigger = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		inTrigger = false;
	}

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.Space) && BattleSystemManager.playerDefended)
        {

			Debug.Log("Countered");
			BattleSystemManager.countered = true;
            Player.anim.SetTrigger("Slash");

        }
        
		
    }
}
