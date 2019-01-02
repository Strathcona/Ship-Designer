﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PartLoader : MonoBehaviour {
    public static PartLoader instance;
    public GameObject partLoadPopup;
    public GameObject selectableFullPartDisplayPrefab;
    public GameObject partDisplayRoot;
    private GameObjectPool pool;
    private Action<Part> onPartLoaded;
    private Action noSelection;

    public SelectableFullPartDisplay selectedDisplay;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another part loader somewhere...");
        }

        selectableFullPartDisplayPrefab = Resources.Load("Prefabs/Selectable Full Part Display", typeof(GameObject)) as GameObject;
        pool = new GameObjectPool(selectableFullPartDisplayPrefab, partDisplayRoot);
    }


    public void SelectDisplay(SelectableFullPartDisplay display) {
        if(selectedDisplay != null) {
            selectedDisplay.SetOutline(false);  
        }
        selectedDisplay = display;
        display.SetOutline(true);
    }

    public void LoadPart() {
        if(selectedDisplay != null) {
            onPartLoaded(selectedDisplay.part);
            Clear();
            partLoadPopup.SetActive(false);
        }
    }

    public void Cancel() {
        noSelection?.Invoke();
        partLoadPopup.SetActive(false);
        Clear();
    }

    public void LoadPartPopup(Action<Part> _onPartLoaded, Action _noSelection=null) {
        onPartLoaded = _onPartLoaded;
        noSelection = _noSelection;
        partLoadPopup.SetActive(true);
        List<Part> parts = PartLibrary.GetParts();
        foreach (Part p in parts) {
            GameObject g = pool.GetGameObject();
            SelectableFullPartDisplay s = g.GetComponent<SelectableFullPartDisplay>();
            s.DisplayPart(p);
            var val = s;
            s.button.onClick.AddListener(delegate { SelectDisplay(val); });
        }
    }

    public void Clear() {
        pool.ReleaseAll();
    }
}