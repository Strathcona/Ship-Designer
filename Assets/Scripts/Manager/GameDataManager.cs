using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour, IInitialized {
    public static GameDataManager instance;
    public List<GalaxyEntity> entities = new List<GalaxyEntity>();
    public GalaxyData masterGalaxyData;

    public void Initialize() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another GameDataManager somewhere");
        }
        masterGalaxyData = new GalaxyData();
    }

    public void ClearAllEntities() {
        foreach(GalaxyEntity e in entities) {
            if(e != null) {
                e.ClearEntityTerritory();
            }
        }
        entities.Clear();
    }
}
