using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    [SerializeField] List<string> lines; //stores the dialogue lines

    public List<string> Lines
    {
        get { return lines; }
    }
}
