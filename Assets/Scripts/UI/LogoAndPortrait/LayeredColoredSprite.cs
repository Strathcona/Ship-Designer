using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class LayeredColoredSprite : IHasLayeredSprites {
    private Sprite[] sprites;
    public Sprite[] Sprites {
        get { return sprites; }
    }
    private Color[] colors;
    public Color[] Colors {
        get { return colors; }
    }
    public event Action<IHasLayeredSprites> OnIHasLayeredSpriteChangeEvent;

    public bool SetSprite(int i, Sprite sprite) {
        if(i < sprites.Length) {
            sprites[i] = sprite;
            OnIHasLayeredSpriteChangeEvent?.Invoke(this);
            return true;
        } else {
            return false;
        }
    }
    public bool SetColor(int i, Color color) {
        if (i < colors.Length) {
            colors[i] = color;
            OnIHasLayeredSpriteChangeEvent?.Invoke(this);
            return true;
        } else {
            return false;
        }
    }

    public LayeredColoredSprite(int layers) {
        sprites = new Sprite[layers];
        colors = new Color[layers];
        for(int i = 0; i < layers; i++) {
            colors[i] = Color.white;
        }
    }
}
