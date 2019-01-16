using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeWarpControls : MonoBehaviour {
    public Text timeWarpText;
    public GameObject pauseImage;

    private void Start() {
        if(timeWarpText == null) {
            timeWarpText = GetComponentInChildren<Text>();
        }
        if(pauseImage == null) {
            pauseImage = GetComponentInChildren<Image>().gameObject;
        }
        pauseImage.SetActive(false);
        timeWarpText.text = "▶";
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            TimeManager.instance.SetWarp(1);
            timeWarpText.text = "▶";
            pauseImage.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            TimeManager.instance.SetWarp(2);
            timeWarpText.text = "▶▶";
            pauseImage.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            TimeManager.instance.SetWarp(3);
            timeWarpText.text = "▶▶▶";
            pauseImage.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            TimeManager.instance.SetWarp(4);
            timeWarpText.text = "▶▶▶▶";
            pauseImage.SetActive(false);
        }
        if (Input.GetKey(KeyCode.BackQuote)) {
            TimeManager.instance.SetWarp(0);
            timeWarpText.text = "❚ ❚";
            pauseImage.SetActive(true);
        }
    }

}
