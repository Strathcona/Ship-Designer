using UnityEngine;
using System.Collections.Generic;

public class GalaxyEntityManager : MonoBehaviour {
    public static GalaxyEntityManager instance;
    public GalaxyMap map;
    public List<GalaxyEntity> entities = new List<GalaxyEntity>();
    public Dictionary<int, GalaxyEntity> entityIDs = new Dictionary<int, GalaxyEntity>();
    public int lastID = 0;
    private int numberOfEntitites = 20;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another galaxy entity manager somewhere...");
        }
        map.Initialize();
        CreateInitialEntities();
        DistributeTerritory();
    }

    public void CreateInitialEntities() {
        //entity IDs start at 1, 0 is no owner;
        for(int i=1; i < numberOfEntitites; i++) {
            GalaxyEntity g = new GalaxyEntity(i);
            entities.Add(g);
            entityIDs.Add(i, g);
            lastID = i;
        }
    }

    public void DistributeTerritory() {
        List<GalaxyTile> unclaimedTiles = map.allGalaxyTiles.FindAll(i => i.Owner == null && i.systemCount != 0);

        foreach (GalaxyEntity g in entities) {
            int index = Random.Range(0, unclaimedTiles.Count);
            GalaxyTile startTile = unclaimedTiles[index];
            unclaimedTiles.Remove(startTile);
            g.GainTerritory(startTile);
            for(int i = 0; i < Random.Range(1,4); i++) {
                int territoryCount = g.territory.Count;
                for(int j = 0; j < territoryCount; j++) {
                    foreach(GalaxyTile n in g.territory[j].neighbours) {                        
                        if (n.Owner == null && n.systemCount != 0) {
                            g.GainTerritory(n);
                            unclaimedTiles.Remove(n);
                            //Debug.Log("Adding Territory to Entity " + g.ID + ", " + unclaimedTiles.Count + " remaining");
                        }
                    }
                }
            }
        }
    }
}
