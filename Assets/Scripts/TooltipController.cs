/**
 * Based on Code Monkey's Dynamic Tooltip in Unity video:
 * https://www.youtube.com/watch?v=YUIohCXt_pc
 */

using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour {
    
    public static TooltipController Instance { get; private set; }

    private RectTransform canvasRectTransform;
    private RectTransform tooltipRectTransform;
    private RectTransform textRectTransform;
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Found more than one instance of TooltipController!");
        }
        canvasRectTransform = GameObject.FindWithTag("Canvas").GetComponent<RectTransform>();
        tooltipRectTransform = GetComponent<RectTransform>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textRectTransform = transform.Find("Text").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        
        HideTooltip();
    }

    private void SetText(string tooltipText) {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize + textRectTransform.anchoredPosition * 2;
    }

    private void LateUpdate() {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        tooltipRectTransform.anchoredPosition = anchoredPosition;
        
        tooltipRectTransform.transform.SetAsLastSibling();
    }

    public static void ShowTooltip(string text) {
        Instance.gameObject.SetActive(true);
        Instance.SetText(text);
    }

    public static void HideTooltip() {
        Instance.gameObject.SetActive(false);
    }
}
