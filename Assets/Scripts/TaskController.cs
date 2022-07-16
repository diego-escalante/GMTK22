using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour {
    
    [field: SerializeField] public Task Task { get; private set; }

    private void Awake() {
        if (Task == null) {
            Debug.LogError("No Task attached to TaskController!");
        }
    }
    
    // TODO: Gather roll results and do something with them.
    
}
