using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentScreenTextDisplay : MonoBehaviour
{
    public Text text;

    private void Start() {
        if (text == null) {
            text = GetComponent<Text>();
            if (text == null) {
                Debug.LogError("Time Text Display couldn't find Text on " + gameObject.name);
            }
        }
        ScreenManager.instance.OnScreenChangeEvent += RefreshScreenName;
    }

    public void RefreshScreenName() {
        text.text = ScreenManager.instance.GetCurrentCanvasName();
    }
}
