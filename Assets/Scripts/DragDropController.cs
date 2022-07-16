using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private Vector2 prevPos;

    private void Awake() {
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        prevPos = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        
        Canvas tempCanvas = gameObject.AddComponent<Canvas>();
        tempCanvas.overrideSorting = true;
        tempCanvas.sortingOrder = 5;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Destroy(GetComponent<Canvas>());

        GameObject otherHuman = null;
        GameObject otherSlot = null;

        foreach (GameObject other in eventData.hovered) {
            switch (other.tag) {
                case "Human":
                    otherHuman = other;
                    break;
                case "Human Slot":
                    otherSlot = other;
                    break;
            }
        }

        if (otherHuman != null) {
            // Swap places with other human.
            Transform tempParent = transform.parent;
            InsertHumanInSlot(transform, otherHuman.transform.parent);
            InsertHumanInSlot(otherHuman.transform, tempParent);
        } else if (otherSlot != null) {
            // Insert human to slot.
            InsertHumanInSlot(transform, otherSlot.transform);
        }

        // Human wasn't dragged to a valid spot. Reset position.
        rectTransform.anchoredPosition = prevPos;
    }

    private static void InsertHumanInSlot(Transform human, Transform slot) {
        human.SetParent(slot);
        human.SetAsLastSibling();
        human.localPosition = Vector3.zero;
    }
}
