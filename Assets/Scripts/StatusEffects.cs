using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{

    public static GameObject poisonUI;

    // Start is called before the first frame update
    void Start()
    {
       // poisonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void Poison()
   {
      WhittleClav.Health -= 2;
    }
}
