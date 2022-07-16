using UnityEngine;
using UnityEngine.UI;

public class DisplayHuman : MonoBehaviour {
    
    private Image head;
    private Image hair;
    private Image facialHair;

    private HumanController humanController;

    private void Awake() {
        head = transform.Find("Head").GetComponent<Image>();
        hair = transform.Find("Hair").GetComponent<Image>();
        facialHair = transform.Find("Facial Hair").GetComponent<Image>();
        humanController = GetComponent<HumanController>();
    }

    private void Start() {
        UpdateHuman();
    }

    private void UpdateHuman() {
        Human human = humanController.Human;
        
        if (human.Head != null) {
            head.enabled = true;
            head.sprite = human.Head;
            head.color = human.SkinTone;
        } else {
            head.enabled = false;
        }

        if (human.FacialHair != null) {
            facialHair.enabled = true;
            facialHair.sprite = human.FacialHair;
            facialHair.color = human.HairColor;
        }
        else {
            facialHair.enabled = false;
        }

        if (human.HairStyle != null) {
            hair.enabled = true;
            hair.sprite = human.HairStyle;
            hair.color = human.HairColor;
        } else {
            hair.enabled = false;
        }
    }
}
