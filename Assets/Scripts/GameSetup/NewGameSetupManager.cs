using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameSetupManager : MonoBehaviour {
    public GalaxyDataGenerator galaxyDataGenerator;
    public GameObject galaxyDataScreen;
    public GalaxyEntityGenerator galaxyEntityGenerator;
    public GameObject galaxyEntityScreen;

    private void Start() {
        TimeManager.instance.LockPaused(true);
    }

    public void StartNewGame() {
        ToggleVisible(false);
        galaxyDataScreen.SetActive(true);
    }

    public void DisplayGalaxyEntityGenerator() { 
        ToggleVisible(false);
        galaxyEntityScreen.SetActive(true);
        galaxyEntityGenerator.GetDataFromPreview();
    }

    public void ToggleVisible(bool on) {
        galaxyDataScreen.SetActive(on);
        galaxyEntityScreen.SetActive(on);
    }
}
