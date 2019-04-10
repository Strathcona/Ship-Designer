using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConstructs;

public class Monarchy: EntityGovernment {
    private static Dictionary<Gender, string[]> titles = new Dictionary<Gender, string[]>() {
        {Gender.Male, new string[]{"King", "Lord", "His Highness" } },
        {Gender.Female, new string[]{"Queen", "Lady", "Her Highness" } },
        {Gender.Nonbinary, new string[]{"Their Highness" } },
        {Gender.None, new string[]{ "Their Highness" } },
        {Gender.ThirdGender, new string[]{"Their Highness"} }
    };
    private static string[] names = new string[] {
        "Kingdom of [NAME]",
        "Realm of [NAME]",
        "[NAME] Czardom"
    };

    public Monarchy() {
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
