using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class HumanFactory {

    private static Action[] actions = Resources.LoadAll<Action>("Actions");
    private static Action emptyAction = GetAction("Empty");

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
        newHuman.die = actions;

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
