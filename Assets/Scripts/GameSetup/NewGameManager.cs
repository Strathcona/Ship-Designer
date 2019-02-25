using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour,  IInitialized {
    Canvas newGameCanvas;
    public GameObject GalaxyGeneration;
    public GalaxyDataGenerator galaxyDataGenerator;

    public void Initialize() {
        StartNewGame();
    }

    public void StartNewGame() {

    }

}
