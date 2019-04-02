using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class NPC {
    public string firstName;
    public string lastName;
    public bool feminine;
    public Sprite sprite;
    public Color spriteColor;
    public string title = "";
    public GalaxyEntity affliation;

    public NPC() {
        if(Random.Range(0,2) == 0) {
            feminine = true;
        } else {
            feminine = false;
        }
        if(Random.Range(0,2) >= 1) {
            firstName = StringLoader.GetAString("firstNamesMasculine");
        } else {
            firstName = StringLoader.GetAString("firstNamesFeminine");
        }
        lastName = StringLoader.GetAString("lastNames");
        sprite = SpriteLoader.GetNPCSprite("Generic");
        spriteColor = Constants.GetRandomPastelColor();
    }
}
