using UnityEngine;
using UnityEngine.UI;

public class DieTooltipController : MonoBehaviour {
    public static DieTooltipController Instance { get; private set; }

    private RectTransform tooltipRectTransform;
    private Image[] sides = new Image[6];

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Found more than one instance of DieTooltipController!");
        }

        for (int i = 0; i < sides.Length; i++) {
            sides[i] = transform.Find("Background").GetChild(i).GetComponent<Image>();
        }
        
        tooltipRectTransform = GetComponent<RectTransform>();
        
        HideTooltip();
    }

    private void SetDie(Action[] actions) {
        if (actions == null || actions.Length != 6) {
            Debug.LogError("Cannot set die tooltip; die input is bad.");
            return;
        }

        for (int i = 0; i < actions.Length; i++) {
            sides[i].sprite = actions[i].icon;
            sides[i].color = actions[i].color;
        }
    }

    public static void ShowTooltip(Action[] die, Vector2 anchorPosition) {
        Instance.gameObject.SetActive(true);
        Instance.tooltipRectTransform.anchoredPosition = anchorPosition;
        Instance.tooltipRectTransform.transform.SetAsLastSibling();
        Instance.SetDie(die);
    }

    public static void HideTooltip() {
        Instance.gameObject.SetActive(false);
    }
}
