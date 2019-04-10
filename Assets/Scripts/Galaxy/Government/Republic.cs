using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConstructs;

public class Republic: EntityGovernment {
    private static Dictionary<Gender, string[]> titles = new Dictionary<Gender, string[]>() {
        {Gender.Male, new string[]{"President"} },
        {Gender.Female, new string[]{"President"} },
        {Gender.Nonbinary, new string[]{ "President" } },
        {Gender.None, new string[]{ "President" } },
        {Gender.ThirdGender, new string[]{ "President" } }
    };
    private static string[] names = new string[] {
        "Republic of [NAME]",
        "[NAME] Republic",
        "[NAME] Alliance"
    };

    public Republic() {
        appointmentPeriod = 4;
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
