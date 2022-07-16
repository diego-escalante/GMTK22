using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Task")]
public class Task : ScriptableObject {
    
    [field: SerializeField][field: Multiline] public string Description { get; private set; }
    [field: SerializeField] public Action action { get; private set; }
    [field: SerializeField] public Action secondaryAction { get; private set; }
    [field: SerializeField] public int SlotCount { get; private set; }
    
}
