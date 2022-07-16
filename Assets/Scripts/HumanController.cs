using System;
using System.Collections;
using UnityEngine;

public class HumanController : MonoBehaviour {
    [field: SerializeField] public Human Human { get; set; }
    public Action CurrentRoll { get; private set; }

    private float sideDisplayDuration = 0.1f;
    private DisplayHuman displayHuman;
    private DragDropController dragDropController;
    private DieHoverTip dieHoverTip;

    private void Awake() {
        Human = HumanFactory.CreateHuman(2);
        displayHuman = GetComponent<DisplayHuman>();
        dragDropController = GetComponent<DragDropController>();
        dieHoverTip = GetComponent<DieHoverTip>();
    }

    private void OnEnable() {
        RollController.SubscribeHuman(this);
    }

    private void OnDisable() {
        RollController.UnsubscribeHuman(this);
        ClearRoll();
    }

    public void StartRoll() {
        StopAllCoroutines();
        dragDropController.enabled = false;
        dieHoverTip.enabled = false;
        StartCoroutine(Rolling());
    }

    public void StopRoll() {
        StopAllCoroutines();
        CurrentRoll = Human.Roll();
        displayHuman.DisplayAction(CurrentRoll);
    }

    public void ClearRoll() {
        StopAllCoroutines();
        CurrentRoll = null;
        displayHuman.UpdateHuman();
        dragDropController.enabled = true;
        dieHoverTip.enabled = true;
    }

    private IEnumerator Rolling() {
        while (true) {
            displayHuman.DisplayAction(Human.Roll());
            yield return new WaitForSeconds(sideDisplayDuration);
        }
    }
}
