using UnityEngine;
using UnityEngine.UI;
using System;

public interface IHasLayeredSprites {

    Sprite[] Sprites {
        get;
    }
    Color[] Colors {
        get;
    }
    bool SetSprite(int i, Sprite sprite);
    bool SetColor(int i, Color color);
    event Action<IHasLayeredSprites> OnIHasLayeredSpriteChangeEvent;

}
