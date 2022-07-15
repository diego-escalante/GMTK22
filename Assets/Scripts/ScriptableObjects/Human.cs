using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Human", menuName = "Human")]
public class Human : ScriptableObject {

    public Color skinColor;
    public Sprite hairStyle;
    public Color hairColor;
    public Action[] die = new Action[6];
    
}


