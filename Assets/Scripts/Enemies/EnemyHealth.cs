using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth: MonoBehaviour
{
    public Image healthBarImage;

    public WhittleClav enemy;

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(WhittleClav.Health / enemy.maxHealth, 0, 1f);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
