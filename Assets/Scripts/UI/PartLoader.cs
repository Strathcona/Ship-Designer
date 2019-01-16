using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PartLoader : MonoBehaviour {
    public static PartLoader instance;
    public GameObject partLoadPopup;
    public GameObject selectableFullPartDisplayPrefab;
    public GameObject partDisplayRoot;
    public Text partLoadLabel;
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

        selectableFullPartDisplayPrefab = Resources.Load("Prefabs/Full Part Display", typeof(GameObject)) as GameObject;
        pool = new GameObjectPool(selectableFullPartDisplayPrefab, partDisplayRoot);
    }


    public void SelectDisplay(SelectableFullPartDisplay display) {
        if(selectedDisplay != null) {
            selectedDisplay.Deselect();  
        }
        selectedDisplay = display;
        display.Select();
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

    public void LoadPartPopup(Action<Part> _onPartLoaded, Action _noSelection=null, string label="", bool displayNonDeveloped=false) {
        onPartLoaded = _onPartLoaded;
        noSelection = _noSelection;
        if(label != "") {
            partLoadLabel.text = label;
        }
        partLoadPopup.SetActive(true);
        List<Part> parts = PartLibrary.GetParts();
        foreach (Part p in parts) {
            GameObject g = pool.GetGameObject();
            SelectableFullPartDisplay s = g.GetComponent<SelectableFullPartDisplay>();
            s.DisplayPart(p);
            var val = s;
            s.button.onClick.AddListener(delegate { SelectDisplay(val); });
        }
        if (displayNonDeveloped) {
            parts = PartLibrary.GetUndevelopedParts();
            foreach (Part p in parts) {
                GameObject g = pool.GetGameObject();
                SelectableFullPartDisplay s = g.GetComponent<SelectableFullPartDisplay>();
                s.DisplayPart(p);
                var val = s;
                s.button.onClick.AddListener(delegate { SelectDisplay(val); });
            }
        }
    }

    public void Clear() {
        pool.ReleaseAll();
    }
}
