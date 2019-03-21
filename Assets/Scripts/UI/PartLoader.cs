using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PartLoader : MonoBehaviour {
    public static PartLoader instance;
    public GameObject partLoadPopup;
    private Action<Part> onPartLoaded;
    private Action noSelection;
    public PartList partList;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another part loader somewhere...");
        }
        partLoadPopup.SetActive(false);
    }

    public void LoadPart() {
        if(partList.selectedDisplay != null) {
            onPartLoaded.Invoke(partList.selectedDisplay.part);
            partLoadPopup.SetActive(false);
        }
    }

    public void Cancel() {
        noSelection?.Invoke();
        partLoadPopup.SetActive(false);
    }

    public void LoadPartPopup(Action<Part> onPartLoaded, Action noSelection=null) {
        this.onPartLoaded = onPartLoaded;
        this.noSelection = noSelection;
        partLoadPopup.SetActive(true);
        Part[] parts = PlayerManager.instance.activePlayer?.ActiveCompany?.engineeringDepartment.AllParts;
        partList.DisplayParts(parts);
    }
}
