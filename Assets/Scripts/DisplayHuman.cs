using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHuman : MonoBehaviour {

    [SerializeField]
    private Human human;
    public Human Human {
        get => human;
        set {
            human = value;
            UpdateHuman();
        }
    }

    private Image head;
    private Image hair;
    private Image facialHair;


    private void Awake() {
        head = transform.Find("Head").GetComponent<Image>();
        hair = transform.Find("Hair").GetComponent<Image>();
        facialHair = transform.Find("Facial Hair").GetComponent<Image>();
        UpdateHuman();
    }

    private void UpdateHuman() {
        if (human.Head != null) {
            head.enabled = true;
            head.sprite = Human.Head;
            head.color = Human.SkinTone;
        } else {
            head.enabled = false;
        }

        if (human.FacialHair != null) {
            facialHair.enabled = true;
            facialHair.sprite = Human.FacialHair;
            facialHair.color = Human.HairColor;
        }
        else {
            facialHair.enabled = false;
        }

        if (human.HairStyle != null) {
            hair.enabled = true;
            hair.sprite = Human.HairStyle;
            hair.color = Human.HairColor;
        } else {
            hair.enabled = false;
        }
    }
}
