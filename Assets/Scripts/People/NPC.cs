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

    public NPC(Species species, Gender gender, string firstName, string lastName) {
        this.species = species;
        this.gender = gender;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public static NPC GetChild(NPC parent) {
        Species species = parent.species;
        Gender gender = species.genders[Random.Range(0, species.genders.Length)];
        string firstName = "";
        if (gender == Gender.Male) {
            firstName = StringLoader.GetAString("FirstNamesMasculine");
        } else if (gender == Gender.Female) {
            firstName = StringLoader.GetAString("FirstNamesFeminine");
        } else {
            firstName = StringLoader.GetAString(new string[] { "FirstNamesFeminine", "FirstNamesMasculine" });
        }
        string lastName = parent.LastName;
        return new NPC(species, gender, firstName, lastName);
    }
}
