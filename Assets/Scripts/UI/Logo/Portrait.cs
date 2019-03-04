using UnityEngine;
using System.Collections;
using System;

public class Portrait {
    private Sprite layer1;
    public Sprite Layer1 {
        get { return layer1; }
        set {
            layer1 = value;
            OnPortraitChangedEvent?.Invoke(this);
        }
    }

    private Color color1;
    public Color Color1 {
        get { return color1; }
        set {
            color1 = value;
            OnPortraitChangedEvent?.Invoke(this);
        }
    }

    public event Action<Portrait> OnPortraitChangedEvent;
}
