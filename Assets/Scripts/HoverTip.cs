using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField][Multiline] private string text;
    private float showDelay = 1f;

    public void OnPointerEnter(PointerEventData _) {
        StartCoroutine(ShowAfterDelay());
    }

    public void OnPointerExit(PointerEventData _) {
        StopAllCoroutines();
        TooltipController.HideTooltip();
    }

    private IEnumerator ShowAfterDelay() {
        yield return new WaitForSeconds(showDelay);
        TooltipController.ShowTooltip(text);
    }
}
