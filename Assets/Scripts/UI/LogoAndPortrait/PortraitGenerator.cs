﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class PortraitGenerator: MonoBehaviour {
    private LayeredColoredSprite portrait;
    public LayeredColoredSprite Portrait {
        get { return portrait; }
    }
    public LayeredColoredSpriteDisplay portraitDisplay;
    public Button cycleLayer1;
    public Button cycleColor1;
    private int layer1Index = 0;
    private int color1Index = 0;
    public List<Sprite> layer1Sprites = new List<Sprite>();

    public List<Color> layerColors = new List<Color>() {
        Color.red,
        Color.magenta,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.white,
        Color.grey
    };

    public event Action<LayeredColoredSprite> OnLayeredSpriteChangeEvent;

    public void SetSpecies(Species s) {
        portrait = new LayeredColoredSprite(1);
        portraitDisplay.DisplayLayeredColoredSprite(portrait);
        layer1Sprites.Clear();
        layer1Sprites.AddRange(s.portraitRange);
        layerColors.Clear();
        layerColors.AddRange(s.SpeciesColor());
        layer1Index = UnityEngine.Random.Range(0, layer1Sprites.Count - 1);
        color1Index = UnityEngine.Random.Range(0, layerColors.Count - 1);
        CycleLayer();
        CycleColor();

    }

    public void SetSpriteSet(Sprite[] sprites) {
        layer1Sprites = new List<Sprite>(sprites);
        CycleLayer();
    }

    public void CycleLayer() {
        if(layer1Sprites.Count > 0) {
            if (layer1Index >= layer1Sprites.Count - 1) {
                layer1Index = 0;
            } else {
                layer1Index += 1;
            }
            portrait.SetSprite(0, layer1Sprites[layer1Index]);
            cycleLayer1.GetComponentInChildren<Text>().text = layer1Index.ToString();
        }
        OnLayeredSpriteChangeEvent?.Invoke(portrait);
    }

    public void CycleColor() {
        if(layerColors.Count > 0) {
            if (color1Index >= layerColors.Count - 1) {
                color1Index = 0;
            } else {
                color1Index += 1;
            }
            portrait.SetColor(0, layerColors[color1Index]);
            cycleColor1.GetComponent<Image>().color = portrait.Colors[0];
        }
        OnLayeredSpriteChangeEvent?.Invoke(portrait);
    }
}
