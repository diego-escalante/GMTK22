using System.Collections;
using System.Collections.Generic;
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
        transform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / transform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        foreach (GameObject other in eventData.hovered) {
            switch (other.tag) {
                case "Human":
                    // Swap places with other human.
                    Transform tempParent = transform.parent;
                    InsertHumanInSlot(transform, other.transform.parent);
                    InsertHumanInSlot(other.transform, tempParent);
                    return;
                case "Human Slot":
                    // Insert human to slot.
                    InsertHumanInSlot(transform, other.transform);
                    return;
                default:
                    // Skip unrecognized objects.
                    continue;
            }
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
