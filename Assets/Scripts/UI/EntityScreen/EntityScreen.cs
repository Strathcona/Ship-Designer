using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScreen : MonoBehaviour {
    public static EntityScreen instance;
    public GalaxyEntity galaxyEntity;
    public GameObject mainScreen;
    public GameObject intelScreen;
    public GameObject contractScreen;
    public GameObject conversationScreen;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another entity screen somewhere...");
        }
    }

    public void DisplayEntity(GalaxyEntity _galaxyEntity) {
        galaxyEntity = _galaxyEntity;
        gameObject.SetActive(true);
    }

    public void Close() {
        galaxyEntity = null;
        gameObject.SetActive(false);
    }

    public void ShowMainScreen() {
        ToggleVisible(false);
        mainScreen.SetActive(true);
    }

    public void ShowContractScreen() {
        ToggleVisible(false);
        contractScreen.SetActive(true);
    }

    public void ToggleVisible(bool on) {
        mainScreen.SetActive(on);
        intelScreen.SetActive(on);
        contractScreen.SetActive(on);
        conversationScreen.SetActive(on);
    }

}
