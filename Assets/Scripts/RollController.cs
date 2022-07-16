using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RollController : MonoBehaviour {

    private static RollController instance;
    private List<HumanController> humanControllers = new ();

    private float stopRollIterationDuration = 0.2f;
    private float ClearRollIterationDuration = 0.1f;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Found more than one instance of the GameController!");
        }
    }

    public static void SubscribeHuman(HumanController humanController) {
        if (!instance.humanControllers.Contains(humanController)) {
            instance.humanControllers.Add(humanController);
        }
    }

    public static void UnsubscribeHuman(HumanController humanController) {
        instance.humanControllers.Remove(humanController);
    }

    public static void StartRolls() {
        DieTooltipController.HideTooltip();
        instance.StopAllCoroutines();
        foreach (HumanController human in instance.humanControllers) {
            human.StartRoll();
        }
    }
    
    public static void StopRolls() {
        instance.StopAllCoroutines();
        List<System.Action> functionsToRun = new();
        foreach (HumanController human in instance.humanControllers) {
            functionsToRun.Add(human.StopRoll);
        }
        instance.StartCoroutine(instance.IterateRolls(instance.stopRollIterationDuration, functionsToRun));
    }
    
    public static void ClearRolls() {
        instance.StopAllCoroutines();
        List<System.Action> functionsToRun = new();
        foreach (HumanController human in instance.humanControllers) {
            functionsToRun.Add(human.ClearRoll);
        }
        instance.StartCoroutine(instance.IterateRolls(instance.ClearRollIterationDuration, functionsToRun));
    }
    
    private IEnumerator IterateRolls(float duration, List<System.Action> functionsToRun) {
        Shuffle(functionsToRun);
        foreach (System.Action functionToRun in functionsToRun) {
            yield return new WaitForSeconds(duration);
            functionToRun();
        }
    }

    // TODO: This is for testing, and should be removed. This script should be leveraged
    //  elsewhere and inputs need to be managed separately.
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            StartRolls();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            StopRolls();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ClearRolls();
        }
    }

    private static void Shuffle<T>(IList<T> list) {
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n+1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
