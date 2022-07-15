using UnityEngine;

public class HelloWorld : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Camera.main.backgroundColor = new Color(Random.value, Random.value, Random.value);
        }
    }
}
