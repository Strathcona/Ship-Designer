using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LogoGenerator: MonoBehaviour {
    private Logo logo;
    public Button cycleLayer1;
    public Button cycleLayer2;
    public Button cycleLayer3;

    public Button cycleColor1;
    public Button cycleColor2;
    public Button cycleColor3;

    private int layer1Index = 0;
    private List<Sprite> layer1Sprites = new List<Sprite>();
    private int layer2Index = 0;
    private List<Sprite> layer2Sprites = new List<Sprite>();
    private int layer3Index = 0;
    private List<Sprite> layer3Sprites = new List<Sprite>();

    public LogoDisplay logoDisplay;

    private int color1Index = 0;
    private int color2Index = 0;
    private int color3Index = 0;
    private List<Color> layerColors = new List<Color>() {
        Color.red,
        Color.magenta,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.white,
        Color.black
    };

    private void Start() {
        logo = new Logo();
        logoDisplay.DisplayLogo(logo);
        layer1Sprites = SpriteLoader.GetAllSymbolParts("Back");
        layer2Sprites = SpriteLoader.GetAllSymbolParts("Mid");
        layer3Sprites = SpriteLoader.GetAllSymbolParts("Front");

        layer1Index = UnityEngine.Random.Range(0, layer1Sprites.Count - 1);
        layer2Index = UnityEngine.Random.Range(0, layer2Sprites.Count - 1);
        layer3Index = UnityEngine.Random.Range(0, layer3Sprites.Count - 1);
        color1Index = UnityEngine.Random.Range(0, layerColors.Count - 1);
        color2Index = UnityEngine.Random.Range(0, layerColors.Count - 1);
        color3Index = UnityEngine.Random.Range(0, layerColors.Count - 1);

        CycleLayer(1);
        CycleLayer(2);
        CycleLayer(3);
        CycleColor(1);
        CycleColor(2);
        CycleColor(3);
    }

    public void CycleLayer(int layer) {
        if(layer == 1) {
            if(layer1Index >= layer1Sprites.Count-1) {
                layer1Index = 0;
            } else {
                layer1Index += 1;
            }
            logo.Layer1 = layer1Sprites[layer1Index];
            cycleLayer1.GetComponentInChildren<Text>().text = layer1Index.ToString();
        } else if (layer == 2) {
            if (layer2Index >= layer2Sprites.Count-1) {
                layer2Index = 0;
            } else {
                layer2Index += 1;
            }
            logo.Layer2 = layer2Sprites[layer2Index];
            cycleLayer2.GetComponentInChildren<Text>().text = layer2Index.ToString();
        }
        if (layer == 3) {
            if (layer3Index >= layer3Sprites.Count-1) {
                layer3Index = 0;
            } else {
                layer3Index += 1;
            }
            logo.Layer3 = layer3Sprites[layer3Index];
            cycleLayer3.GetComponentInChildren<Text>().text = layer3Index.ToString();
        }
    }

    public void CycleColor(int layer) {
        if (layer == 1) {
            if (color1Index >= layerColors.Count-1) {
                color1Index = 0;
            } else {
                color1Index += 1;
            }
            logo.Color1 = layerColors[color1Index];
            cycleColor1.GetComponent<Image>().color = logo.Color1;
        } else if (layer == 2) {
            if (color2Index >= layerColors.Count-1) {
                color2Index = 0;
            } else {
                color2Index += 1;
            }
            logo.Color2 = layerColors[color2Index];
            cycleColor2.GetComponent<Image>().color = logo.Color2;
        } else if (layer == 3) {
            if (color3Index >= layerColors.Count-1) {
                color3Index = 0;
            } else {
                color3Index += 1;
            }
            logo.Color3 = layerColors[color3Index];
            cycleColor3.GetComponent<Image>().color = logo.Color3;
        }
    }
}
