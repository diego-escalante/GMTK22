using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DieHoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Human human;
    private RectTransform rectTransform;
    
    private static RectTransform canvasRectTransform;

    private void Start() {
        if (canvasRectTransform == null) {
            canvasRectTransform = GameObject.FindWithTag("Canvas").GetComponent<RectTransform>();
        }

        rectTransform = GetComponent<RectTransform>();
        human = GetComponent<HumanController>().Human;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Vector2 pos = rectTransform.position;
        pos.y = canvasRectTransform.rect.height / 2 > pos.y ? pos.y + 384 + 64 - 32 : pos.y - 32;
        
        DieTooltipController.ShowTooltip(human.Die, pos);
        
    }

    public void OnPointerExit(PointerEventData _) {
        StopAllCoroutines();
        DieTooltipController.HideTooltip();
    }
}
