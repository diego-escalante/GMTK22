using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [field: SerializeField][field: Multiline] public string Text { get; set; }
    private float showDelay = 0.5f;

    public void OnPointerEnter(PointerEventData _) {
        StartCoroutine(ShowAfterDelay());
    }

    public void OnPointerExit(PointerEventData _) {
        StopAllCoroutines();
        TooltipController.HideTooltip();
    }

    private IEnumerator ShowAfterDelay() {
        yield return new WaitForSeconds(showDelay);
        TooltipController.ShowTooltip(Text);
    }
}
