﻿using UnityEngine;
using System.Collections;
using GameConstructs;

public static class SpeciesGenerator {

    public static Species GetSpecies(string name="") {
        GradientColorKey[] colorKeys = new GradientColorKey[3];
        float position = 0.0f;
        for (int i = 0; i < 3; i++) {
            colorKeys[i] = new GradientColorKey(Constants.GetRandomPastelColor(), position);
            position += 0.50f;
        }
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2] {
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        };
        Gradient g = new Gradient();
        g.alphaKeys = alphaKeys;
        g.colorKeys = colorKeys;
        Sprite[] sprites = SpriteLoader.GetRandomSetOfAlienSprites().ToArray();
        if(name == "") {
            name = MarkovGenerator.GenerateMarkovWord(StringLoader.GetAllStrings("SpeciesButterfly"), 1)[0];
        }
        return new Species(name, sprites, g);
    }

    public static Species[] GetMultipleSpecies(int number) {
        Species[] species = new Species[number];
        string[] names = MarkovGenerator.GenerateMarkovWord(StringLoader.GetAllStrings("SpeciesButterfly"), 400);
        for (int i = 0; i < number; i++) {
            species[i] = GetSpecies(names[i]);
        }
        return species;
    }
}

