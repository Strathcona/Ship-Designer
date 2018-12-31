using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PartLoader : MonoBehaviour {
    public static PartLoader instance;
    public GameObject partLoadPopup;
    public GameObject selectableFullPartDisplayPrefab;
    public GameObject partDisplayRoot;
    private GameObjectPool pool;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another part loader somewhere...");
        }

        selectableFullPartDisplayPrefab = Resources.Load("Prefabs/Selectable Full Part Display", typeof(GameObject)) as GameObject;
        pool = new GameObjectPool(selectableFullPartDisplayPrefab, partDisplayRoot, SetupAction);
    }

    public void SetupAction(GameObject g) {

    }

    public void LoadPartPopup(Action<Part> onPartLoaded, Action noSelection=null) {
        partLoadPopup.SetActive(true);
        List<Part> parts = PartLibrary.GetParts();
        foreach (Part p in parts) {
            GameObject g = pool.GetGameObject();
            g.GetComponent<SelectableFullPartDisplay>().DisplayPart(p);
        }
    }

    public void Clear() {
        pool.ReleaseAll();
    }
}
