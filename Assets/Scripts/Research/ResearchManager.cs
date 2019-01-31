using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager: MonoBehaviour {
    public static ResearchManager instance;
    public Dictionary<string, int> researchValues = new Dictionary<string, int>() {
        {"Engine MaxThrust MaxValue", 150 }
    };

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another research manager somewhere...");
        }
    }
    public static void SetEffect(string effect) {

    }

    public int GetResearchValue(string key) {
        return researchValues[key];
    }

}
