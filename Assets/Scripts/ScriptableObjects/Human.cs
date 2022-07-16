using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Human", menuName = "Human")]
public class Human : ScriptableObject {

    [field: SerializeField] public Sprite Head { get; set; }
    [field: SerializeField] public Color SkinTone { get; set; }
    [field: SerializeField] public Sprite HairStyle { get; set; }
    [field: SerializeField] public Sprite FacialHair { get; set; }
    [field: SerializeField] public Color HairColor { get; set; }
    [field: SerializeField] public Action[] Die { get; set; } = new Action[6];

    public Action Roll() {
        return Die[Random.Range(0, 6)];
    }
    
}


