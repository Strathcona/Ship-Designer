using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager: MonoBehaviour {
    public static ResearchManager instance;
    public Dictionary<string, int> researchValues = new Dictionary<string, int>() {
        {"Engine Maximum Thrust MinValue", 100 },
        {"Engine Maximum Thrust MaxValue", 120 },
        {"Engine Agility MinValue", 1 },
        {"Engine Agility MaxValue", 20 },
        {"Engine Thrust MinValue", 1 },
        {"Engine Thrust MaxValue", 20 },
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
        {"Reactor Maximum Output MinValue", 100 },
        {"Reactor Maximum Output MaxValue", 120 },
        {"Sensor Range MinValue", 1 },
        {"Sensor Range MaxValue", 20 },
        {"Sensor Resolution MinValue", 1 },
        {"Sensor Resolution MaxValue", 20 },
        {"Weapon Caliber MinValue", 5 },
        {"Weapon Caliber MaxValue", 100 },
        {"Weapon Reload Time MinValue", 5 },
        {"Weapon Reload Time MaxValue", 100 },
        {"Weapon Turret Setup MinValue", 0 },
        {"Weapon Turret Setup MaxValue", 4 },
        {"Weapon Weapon Type MinValue", 0 },
        {"Weapon Weapon Type MaxValue", 1 },
        {"Shield Shield Type MinValue", 0 },
        {"Shield Shield Type MaxValue", 1 },
        {"Shield Shield Strength MinValue", 1 },
        {"Shield Shield Strength MaxValue", 20 },
        {"Shield Recharge Time MinValue", 20 },
        {"Shield Recharge Time MaxValue", 100 },
        {"Weapon MaxTier", 1 },
        {"Engine MaxTier", 1 },
        {"Sensor MaxTier", 1 },
        {"FireControl MaxTier", 1 },
        {"Reactor MaxTier", 1 },
        {"Shield MaxTier", 1 }

    };

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another research manager somewhere...");
        }
    }
    public void SetEffect(string effect) {
        string[] args = effect.Split(':');
        switch (args[0]) {
            case "UnlockTier":
                if(int.TryParse(args[2], out int tier)) {
                    researchValues[args[1] + " MaxTier"] += tier;
                } else {
                    Debug.LogError("Couldn't parse effect " + effect);
                }
                break;
            case "AddTweakableMax":
                if (int.TryParse(args[3], out int val)) {
                    researchValues[args[1] + " "+args[2] + " MaxValue"] += val;
                } else {
                    Debug.LogError("Couldn't parse effect " + effect);
                }
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
