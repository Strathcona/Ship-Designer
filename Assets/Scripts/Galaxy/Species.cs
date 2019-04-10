using UnityEngine;
using GameConstructs;
using System.Collections;

public class Species {
    public string adjective = "";
    public Sprite[] portraitRange;
    public Gradient colorRange;
    public Gender[] genders;

    public Species(string adjective, Sprite[] portraitRange, Gradient colorRange) {
        this.adjective = adjective;
        this.portraitRange = portraitRange;
        this.colorRange = colorRange;
        float rand = UnityEngine.Random.Range(0, 1.0f);
        if (rand < 0.85f) {
            genders = new Gender[] {
                Gender.Male,
                Gender.Female,
                Gender.Nonbinary,
                Gender.None
            };
        } else if (rand < 0.98) {
            genders = new Gender[] {
                Gender.Female,
                Gender.None
            };
        } else {
            genders = new Gender[] {
                Gender.Male,
                Gender.Female,
                Gender.ThirdGender,
                Gender.Nonbinary,
                Gender.None
            };
        }
    }

    public Color[] SpeciesColor() {
        return new Color[9]{
        colorRange.Evaluate(0.0f),
        colorRange.Evaluate(0.125f),
        colorRange.Evaluate(0.25f),
        colorRange.Evaluate(0.375f),
        colorRange.Evaluate(0.5f),
        colorRange.Evaluate(0.625f),
        colorRange.Evaluate(0.75f),
        colorRange.Evaluate(0.875f),
        colorRange.Evaluate(1.0f)
        };
    }
}
