using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class NPC: Person {
    
    public NPC() {
        species = GameDataManager.instance.Species[Random.Range(0, GameDataManager.instance.Species.Length)];
        gender = species.genders[Random.Range(0, species.genders.Length)];
        if (gender == Gender.Male) {
            firstName = StringLoader.GetAString("FirstNamesMasculine");
        } else if (gender == Gender.Female) {
            firstName = StringLoader.GetAString("FirstNamesFeminine");
        } else {
            firstName = StringLoader.GetAString(new string[] { "FirstNamesFeminine", "FirstNamesMasculine" });
        }
        lastName = StringLoader.GetAString("LastNames");
    }
}
