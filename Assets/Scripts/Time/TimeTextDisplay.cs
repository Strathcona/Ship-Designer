using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTextDisplay : MonoBehaviour
{
    public Text text;

    private void Awake() {
        if(text == null) {
            text = GetComponent<Text>();
            if(text == null) {
                Debug.LogError("Time Text Display couldn't find Text on " + gameObject.name);
            }
        }
    }

    private void Update() {
        text.text = TimeManager.instance.GetCurrentTimeString();
    }
}
