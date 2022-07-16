using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class HumanFactory {

    private static Action[] actions = Resources.LoadAll<Action>("Actions");
    private static Action emptyAction = GetAction("Empty");

    private static Sprite[] heads = Resources.LoadAll<Sprite>("Sprites/Heads");
    private static Sprite[] hairStyles = Resources.LoadAll<Sprite>("Sprites/HairStyles");
    private static Sprite[] facialHair = Resources.LoadAll<Sprite>("Sprites/FacialHair");

    private static Color[] hairColors = {
        new Color(0.15f, 0.15f, 0.15f, 1),
        new Color(0.6f, 0.6f, 0.6f, 1),
        new Color(0.3301887f, 0.1699953f, 0.01401744f, 1),
        new Color(0.627f, 0.2851415f, 0, 1f),
        new Color(0.8396226f, 0.758382f, 0.3524831f, 1),
        new Color(0.6132076f, 0.1494454f, 0.07809719f, 1),
        new Color(0.7075472f, 0.5917057f, 0.07676221f, 1),
        new Color(0.234f, 0.1599f, 0, 1),
        new Color(0.25f, 0.25f, 0.25f, 1)
    };

    private static Color[] skinTones = {
        new Color(0.574f, 0.4183657f, 0.312867f, 1),
        new Color(0.9137255f, 0.6068082f, 0.4980392f, 1),
        new Color(1f, 0.7317517f, 0.475f, 1),
        new Color(0.362f, 0.2177063f, 0.1392308f, 1),
        new Color(0.562f, 0.3387474f, 0.215246f, 1),
        new Color(0.773f, 0.4548241f, 0.274415f, 1),
        new Color(1f, 0.6614486f, 0.475f, 1),
        new Color(1f, 0.8403288f, 0.752f, 1)
    };

    public static Human CreateHuman() {
        return CreateHuman(null);
    }
    
    // Create a human with a desired number of empty actions. Empties are placed at the end of the die array.
    public static Human CreateHuman(int numberOfEmpties) {
        numberOfEmpties = Mathf.Clamp(numberOfEmpties, 0, 6);
        Action[] die = new Action[6];
        while (numberOfEmpties > 0) {
            die[die.Length - numberOfEmpties] = emptyAction;
            numberOfEmpties--;
        }
        return CreateHuman(die);
    }

    public static Human CreateHuman(Action[] actions) {
        // If actions is null, create a new empty array.
        if (actions == null) {
            actions = new Action[6];
        }
        
        // Generate the human's action die.
        for (int i = 0; i < actions.Length; i++) {
            if (actions[i] != null) {
                continue;
            }
            actions[i] = GetRandomAction(false);
        }
        
        // Generate a new human.
        Human newHuman = ScriptableObject.CreateInstance<Human>();
        newHuman.Head = heads[Random.Range(0, heads.Length)];
        
        if (Random.value > 0.075f) {
            newHuman.HairStyle = hairStyles[Random.Range(0, hairStyles.Length)];
        }

        if (Random.value > 0.7f) {
            newHuman.FacialHair = facialHair[Random.Range(0, facialHair.Length)];
        }
        
        newHuman.HairColor = hairColors[Random.Range(0, hairColors.Length)];
        newHuman.SkinTone = skinTones[Random.Range(0, skinTones.Length)];
        newHuman.Die = actions;

        return newHuman;
    }

    // TODO: This can loop forever if the only action possible is empty, but this is a game jam.
    private static Action GetRandomAction(bool includeEmpty) {
        Action action;
        do {
            action = actions[Random.Range(0, actions.Length)];
        } while (!includeEmpty && action == emptyAction);
        return action;
    }

    // TODO: If this is called too often, put actions in a dictionary.
    private static Action GetAction(String actionName) {
        for (int i = 0; i < actions.Length; i++) {
            if (actions[i].name == actionName) {
                return actions[i];
            }
        }
        Debug.LogErrorFormat("Could not find action with name {0}", actionName);
        return null;
    }
    
}
