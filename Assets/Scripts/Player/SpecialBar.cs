using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public Image specialBarImage;

    public Player player;

    public void UpdateSpecialBar()
    {
        specialBarImage.fillAmount = Mathf.Clamp(player.bp / player.maxBP, 0, 1f);
    }

}
