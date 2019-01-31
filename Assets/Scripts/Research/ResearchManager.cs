﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager: MonoBehaviour {
    public static ResearchManager instance;
    public Dictionary<string, int> researchValues = new Dictionary<string, int>() {
        {"Engine Maximum Thrust MaxValue", 120 },
        {"Engine Maximum Thrust MinValue", 100 },
        {"Engine Agility MaxValue", 1 },
        {"Engine Agility MinValue", 20 },
        {"Engine Thrust MaxValue", 1 },
        {"Engine Thrust MinValue", 20 },
        {"Engine Energy Efficiency MaxValue", 120 },
        {"Engine Energy Efficiency MinValue", 100 },
        {"Fire Control Tracking MinValue", 1 },
        {"Fire Control Tracking MaxValue", 20 },
        {"Fire Control Accuracy MinValue", 1 },
        {"Fire Control Accuracy MaxValue", 20 },
        {"Fire Control Range MinValue", 1 },
        {"Fire Control Range MaxValue", 20 },
        {"Reactor Output MinValue", 1 },
        {"Reactor Output MaxValue", 20 },
        {"Reactor Maximum Output MinValue", 1 },
        {"Reactor Maximum Output MaxValue", 20 },
        {"Sensor Range MinValue", 1 },
        {"Sensor Range MaxValue", 20 },
        {"Sensor Resolution MinValue", 1 },
        {"Sensor Resolution MaxValue", 20 },
        {"Weapon Caliber MinValue", 5 },
        {"Weapon Caliber MaxValue", 100 },
        {"Weapon Reload MinValue", 5 },
        {"Weapon Reload MaxValue", 100 },
        {"Weapon Turret Setup MinValue", 0 },
        {"Weapon Turret Setup MaxValue", 4 },
        {"Weapon Weapon Type MinValue", 0 },
        {"Weapon Weapon Type MaxValue", 1 },
    };

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another research manager somewhere...");
        }
    }
    public static void SetEffect(string effect) {
        string[] args = effect.Split();
        switch (args[0]) {
            case "UnlockTier":
                Debug.Log("Unlocking Tier " + args[2] + " for " + args[1]);
                break;
            case "AddTweakableMax":
                Debug.Log("Adding " + args[3] + " to " + args[1]+" "+args[2]);
                break;
            case "Nothing":
                Debug.Log("Doing Nothing");
                break;
            default:
                Debug.Log("Couldn't find effect " + args[0]);
                break;
        }
    }

    public int GetResearchValue(string key) {
        if (researchValues.ContainsKey(key)) {
            return researchValues[key];
        } else {
            Debug.LogError("Couldn't find Research Key " + key);
            return 0;
        }
    }

}
