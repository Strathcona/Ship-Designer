using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConstructs;

public class Empire: EntityGovernment {
    private static Dictionary<Gender, string[]> titles = new Dictionary<Gender, string[]>() {
        {Gender.Male, new string[]{"Emperor", "Executor", "Basilieus" } },
        {Gender.Female, new string[]{ "Emperess", "Executor", "Sovriegn" } },
        {Gender.Nonbinary, new string[]{ "Imperator", "Executor", "Sovriegn" } },
        {Gender.None, new string[]{ "Imperator", "Executor", "Sovriegn" } },
        {Gender.ThirdGender, new string[]{ "Imperator", "Executor", "Sovriegn" } }
    };
    private static string[] names = new string[] {
        "Empire of [NAME]",
        "[NAME] Empire",
        "[NAME] Supremacy",
        "[NAME] Imperium",
        "Imperium of [NAME]",
        "[NAME] Czardom"
    };

    public Empire() {
        appointmentPeriod = -1;
    }

    protected override void TransitionToGovernmentImplementation() {
        string name = names[Random.Range(0, names.Length)];
        name = name.Replace("[NAME]", galaxyEntity.name);
        governmentName = name;
    }

    protected override void AppointNewLeaderImplementation() {
        leader.Title = titles[leader.Gender][UnityEngine.Random.Range(0, titles[leader.Gender].Length)];
    }
}
