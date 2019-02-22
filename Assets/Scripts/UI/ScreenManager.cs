using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {
    private static ScreenManager instance;
    public Dictionary<string, Canvas> canvases = new Dictionary<string, Canvas>();

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another Screen Manager Somewhere");
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("MainCanvas")) {
            Canvas c = g.GetComponent<Canvas>();
            if(c == null) {
                Debug.LogError("No canvas on object with MainCanvas Tag "+g.name);
            } else {
                canvases.Add(g.name, c);
            }
        }
        DisableAllCanvases();
    }

    public void DisplayCanvas(string canvasName) {
        DisableAllCanvases();
        if (canvases.ContainsKey(canvasName)) {
            canvases[canvasName].gameObject.SetActive(true);
        }
    }

    private void DisableAllCanvases() {
        foreach(Canvas c in canvases.Values) {
            c.gameObject.SetActive(false);
        }
    }
}
