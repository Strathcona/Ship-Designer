using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTextDisplay : MonoBehaviour
{
    public Text text;

    private void Start() {
        if(text == null) {
            text = GetComponent<Text>();
            if(text == null) {
                Debug.LogError("Time Text Display couldn't find Text on " + gameObject.name);
            }
        }
        TimeManager.instance.OnMinuteEvent += RefreshTime;
    }

    public void RefreshTime() {
        text.text = TimeManager.instance.GetCurrentTimeString();
    }
}
