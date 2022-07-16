using UnityEngine;

public class HumanController : MonoBehaviour {
    [field: SerializeField] public Human Human { get; set; }

    private void Awake() {
        Human = HumanFactory.CreateHuman(3);
    }
}
