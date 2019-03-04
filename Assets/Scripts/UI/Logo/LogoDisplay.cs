using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoDisplay: MonoBehaviour {

    public Image layer1;
    public Image layer2;
    public Image layer3;
    public Logo logo;

    public void DisplayLogo(Logo displayedLogo) {
        logo = displayedLogo;
        layer1.sprite = logo.Layer1;
        layer1.color = logo.Color1;
        layer2.sprite = logo.Layer2;
        layer2.color = logo.Color2;
        layer3.sprite = logo.Layer3;
        layer3.color = logo.Color3;
        logo.OnLogoChangedEvent += RefreshDisplay;
        Debug.Log("Display Logo");
    }

    public void RefreshDisplay(Logo displayedLogo) {
        Debug.Log("Refresh Logo");
        layer1.sprite = logo.Layer1;
        layer1.color = logo.Color1;
        layer2.sprite = logo.Layer2;
        layer2.color = logo.Color2;
        layer3.sprite = logo.Layer3;
        layer3.color = logo.Color3;
    }
}
