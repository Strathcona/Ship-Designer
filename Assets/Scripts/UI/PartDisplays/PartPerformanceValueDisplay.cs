using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartPerformanceValueDisplay : MonoBehaviour, IDisplaysPart
{
    public GameObject root;
    public GameObject performanceValueDisplayPrefab;
    public GameObjectPool performanceValueDisplayPool;
    public Part part;
    public Gradient gradient;
    public GradientColorKey[] colorKeys = new GradientColorKey[3] {
        new GradientColorKey(Color.red, 0.0f),
        new GradientColorKey(Color.yellow, 0.4f),
        new GradientColorKey(Color.green, 1.0f)
    };
    public GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2] {
        new GradientAlphaKey(1.0f, 0.0f),
        new GradientAlphaKey(1.0f,1.0f)
    };

    public void Awake() {
        performanceValueDisplayPool = new GameObjectPool(performanceValueDisplayPrefab, root);
        gradient.alphaKeys = alphaKeys;
        gradient.colorKeys = colorKeys;
    }

    public void DisplayPart(Part p) {
        if (part != null) {
            part.OnPartChangeEvent -= UpdatePartDisplay;
        }
        part = p;
        if(part != null) {
            part.OnPartChangeEvent += UpdatePartDisplay;
            Dictionary<string, float> performanceValues = part.GetNormalizedPerformanceValues();
            foreach (string s in performanceValues.Keys) {
                GameObject g = performanceValueDisplayPool.GetGameObject();
                g.GetComponentInChildren<Text>().text = s;
                g.GetComponentInChildren<Image>().fillAmount = performanceValues[s];
                g.GetComponentInChildren<Image>().color = gradient.Evaluate(performanceValues[s]);

            }
        } else {
            performanceValueDisplayPool.ReleaseAll();
        }
    }

    public void UpdatePartDisplay(Part p) {
        performanceValueDisplayPool.ReleaseAll();
        Dictionary<string, float> performanceValues = part.GetNormalizedPerformanceValues();
        foreach (string s in performanceValues.Keys) {
            GameObject g = performanceValueDisplayPool.GetGameObject();
            g.GetComponentInChildren<Text>().text = s;
            g.GetComponentInChildren<Image>().fillAmount = performanceValues[s];
            g.GetComponentInChildren<Image>().color = gradient.Evaluate(performanceValues[s]);
        }
    }

}
