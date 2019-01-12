using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {
    public GameObject clock;
    public GameObject calendar;
    private Text clockText;
    private Text calendarText;

    private void Start() {
        clockText = clock.transform.GetChild(0).GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        calendarText = calendar.transform.GetChild(0).GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        TimeManager.instance.actionsOnMinute.Add(UpdateTime);
        TimeManager.instance.actionsOnTick.Add(UpdateTick);
    }

    public void UpdateTime() {
        clockText.text = TimeManager.instance.GetTimeString();
    }

    public void UpdateTick(int tick) {
        calendarText.text = "TICK\n" + tick.ToString("D3");
    }
}
