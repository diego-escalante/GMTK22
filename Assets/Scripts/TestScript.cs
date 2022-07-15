using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public Human human;
    // Start is called before the first frame update
    void Start()
    {
        human = HumanFactory.CreateHuman(3);
    }
}
