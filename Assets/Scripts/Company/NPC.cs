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
        firstName = Constants.GetRandomFirstName(feminine);
        lastName = Constants.GetRandomLastName();
        sprite = SpriteLoader.GetNPCSprite("Generic");
        spriteColor = Constants.GetRandomPastelColor();
    }
}
