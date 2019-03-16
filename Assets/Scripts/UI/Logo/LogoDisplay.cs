using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoDisplay: MonoBehaviour {

    public Image layer1;
    public Image layer2;
    public Image layer3;
    public Logo logo;

    public void Start() {
        if(logo == null) {
            SetTransparent();
        }
    }

    public void DisplayLogo(Logo displayedLogo) {
        if (logo == null) {
            SetTransparent();
        }
        logo = displayedLogo;
        layer1.sprite = logo.Layer1;
        layer1.color = logo.Color1;
        layer2.sprite = logo.Layer2;
        layer2.color = logo.Color2;
        layer3.sprite = logo.Layer3;
        layer3.color = logo.Color3;
        logo.OnLogoChangedEvent += RefreshDisplay;
    }

    public void RefreshDisplay(Logo displayedLogo) {
        if (logo == null) {
            SetTransparent();
        }
        Debug.Log("Refresh Logo");
        layer1.sprite = logo.Layer1;
        layer1.color = logo.Color1;
        layer2.sprite = logo.Layer2;
        layer2.color = logo.Color2;
        layer3.sprite = logo.Layer3;
        layer3.color = logo.Color3;
    }

    public void SetTransparent() {
        layer1.color = new Color(0, 0, 0, 0);
        layer2.color = new Color(0, 0, 0, 0);
        layer3.color = new Color(0, 0, 0, 0);
    }
}
