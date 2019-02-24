using UnityEngine;
using System.Collections.Generic;
using GameConstructs;
using System;

public class GalaxyEntityManager : MonoBehaviour, IInitialized {
    public static GalaxyEntityManager instance;
    public GalaxyDisplay display;
    public List<GalaxyEntity> entities = new List<GalaxyEntity>();
    public Dictionary<int, GalaxyEntity> entityIDs = new Dictionary<int, GalaxyEntity>();
    public int lastID = 0;

    public void Initialize() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another galaxy entity manager somewhere...");
        }
        display.Initialize();
        CreateInitialEntities();
    }

    public void CreateInitialEntities() {

    }

    public void DistributeTerritory(GalaxyEntity g, int cycles) {

    }
}
