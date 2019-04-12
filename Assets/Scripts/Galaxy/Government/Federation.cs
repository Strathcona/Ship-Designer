using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConstructs;

public class Federation: EntityGovernment {
    private static string[] titles = new string[]{ "The Honorable", "The Brave", "Protector", "Grand Captain", "Starmaster" };
    private static string[] names = new string[] {
        "[NAME] Accord",
        "[NAME] Federation",
        "[NAME] League",
        "League of [NAME]",
        "[NAME] Accord",
        "[NAME] Cooperative",
        "[NAME] Union"
    };

    public Federation() {
        appointmentPeriod = -1;
    }

    protected override void TransitionToGovernmentImplementation() {
        string name = names[Random.Range(0, names.Length)];
        name = name.Replace("[NAME]", galaxyEntity.name);
        governmentName = name;
    }

    protected override void AppointNewLeaderImplementation() {
        leader.Title = titles[UnityEngine.Random.Range(0, titles.Length)];
    }
}
