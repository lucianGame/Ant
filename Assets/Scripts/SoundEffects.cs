using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour


{
    public AudioSource scrollSound;
	public AudioSource scrollSound2;
	public AudioSource scrollSound3;
	public AudioSource selectSound;

   


    public void Update()

    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
			float random = UnityEngine.Random.Range(0, 3); //Randomly chooses a scroll sound to play when player scrolls up ot down
			switch (random)
			{
				case 0:

					scrollSound.Play();

					break;

				case 1:

					scrollSound2.Play();

					break;

				case 2:

					scrollSound3.Play();

					break;

				default: break;


			}
		}
        
    }

    public void Selection()
    {
        selectSound.Play();
    }
}