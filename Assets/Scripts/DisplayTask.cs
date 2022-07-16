using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTask : MonoBehaviour {

    [SerializeField] private GameObject humanSlotPrefab;
    private TaskController taskController;

    private void Awake() {
        Task task = GetComponent<TaskController>().Task;
        transform.Find("Label").GetComponent<HoverTip>().Text = task.Description;
        transform.Find("Label/Title").GetComponent<TMP_Text>().text = task.name;
        Image image = transform.Find("Label/Icon").GetComponent<Image>();
        if (task.action != null && task.action.icon != null) {
            image.sprite = task.action.icon;
            image.color = task.action.color;
        } else {
            image.gameObject.SetActive(false);    
        }

        Transform secondaryImageTrans = transform.Find("Label/Secondary Icon");
        if (secondaryImageTrans != null) {
            Image secondaryImage = secondaryImageTrans.GetComponent<Image>();
            if (task.secondaryAction != null && task.secondaryAction.icon != null) {
                secondaryImage.gameObject.SetActive(true);
                secondaryImage.sprite = task.secondaryAction.icon;
                secondaryImage.color = task.secondaryAction.color;
            }
        }

        if (humanSlotPrefab == null) {
            Debug.LogError("There is no human slot prefab attached to the DisplayTask script!");
        } else {
            Transform slots = transform.Find("Human Slots");
            
            // Destroy all pre-set children.
            foreach (Transform child in slots) {
                Destroy(child.gameObject);
            }
            
            // Create the desired amount of slots.
            for (int i = 0; i < task.SlotCount; i++) {
                Instantiate(humanSlotPrefab, slots);
            }  
        }
    }

}
