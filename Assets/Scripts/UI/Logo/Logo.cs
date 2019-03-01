using UnityEngine;
using System.Collections;
using System;

public class Logo {
    private Sprite layer1;
    public Sprite Layer1 {
        get { return layer1; }
        set { layer1 = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }
    private Sprite layer2;
    public Sprite Layer2 {
        get { return layer2; }
        set {
            layer2 = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }
    private Sprite layer3;
    public Sprite Layer3 {
        get { return layer3; }
        set {
            layer3 = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }
    private string centerText;
    public string CenterText {
        get { return centerText; }
        set {
            centerText = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }
    private Color color1;
    public Color Color1 {
        get { return color1; }
        set {
            color1 = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }
    private Color color2;
    public Color Color2 {
        get { return color2; }
        set {
            color2 = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }
    private Color color3;
    public Color Color3 {
        get { return color3; }
        set {
            color3 = value;
            OnLogoChangedEvent?.Invoke(this);
        }
    }

    public event Action<Logo> OnLogoChangedEvent;
}
