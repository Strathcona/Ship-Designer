using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScreenManager : MonoBehaviour {
    private static ScreenManager instance;
    public Dictionary<string, Canvas> canvases = new Dictionary<string, Canvas>();
    public Canvas currentCanvas;
    public List<Action> onCanvasChange = new List<Action>();

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
        DisplayCanvas("Room");
    }

    public string GetCurrentCanvasName() {
        return currentCanvas.gameObject.name;
    }

    public void DisplayCanvas(string canvasName) {
        DisableAllCanvases();
        if (canvases.ContainsKey(canvasName)) {
            Canvas c = canvases[canvasName];
            c.gameObject.SetActive(true);
            currentCanvas = c;
        } else {
            Debug.LogError("Couldn't Find Canvas " + canvasName);
        }
        foreach(Action a in onCanvasChange) {
            a();
        }
    }

    private void DisableAllCanvases() {
        foreach(Canvas c in canvases.Values) {
            c.gameObject.SetActive(false);
        }
    }
}
