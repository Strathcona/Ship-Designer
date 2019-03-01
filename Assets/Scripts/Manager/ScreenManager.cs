using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScreenManager : MonoBehaviour, IInitialized {
    public static ScreenManager instance;
    public List<GameObject> canvases = new List<GameObject>();
    public GameObject currentCanvas;
    public event Action OnScreenChangeEvent;

    public void Initialize() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another Screen Manager Somewhere");
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("MainCanvas")) {
            canvases.Add(g);
        }
        DisableAllCanvases();
    }

    public string GetCurrentCanvasName() {
        return currentCanvas.gameObject.name;
    }

    public void DisplayCanvas(string canvasName) {
        DisableAllCanvases();
        GameObject target = canvases.Find(i => i.name == canvasName);
        if (target != null) {
            target.SetActive(true);
            currentCanvas = target;
            OnScreenChangeEvent?.Invoke();
        } else {
            Debug.LogError("Couldn't Find Canvas " + canvasName);
        }
    }

    private void DisableAllCanvases() {
        foreach(GameObject g in canvases) {
            g.SetActive(false);
        }
    }
}
