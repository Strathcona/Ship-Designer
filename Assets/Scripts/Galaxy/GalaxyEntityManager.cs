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
    private int num = 3;

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
        List<Sector> unclaimedTiles = display.allSectors.FindAll(i => i.Owner == null && i.systemCount != 0);
        //entity IDs start at 1, 0 is no owner;
        for (int i=1; i < num; i++) {
            int index = UnityEngine.Random.Range(0, unclaimedTiles.Count);
            Sector startTile = unclaimedTiles[index];
            GalaxyEntity g = GalaxyEntity.GetRandomGalaxyEntity(startTile);
            g.leader = new NPC();
            g.leader.title = g.leaderTitle;
            g.leader.affliation = g;
            entities.Add(g);
            entityIDs.Add(i, g);
            lastID = i;
            DistributeTerritory(g, 4);
        }
    }

    public void DistributeTerritory(GalaxyEntity g, int cycles) {
        List<Sector> unclaimedTiles = display.allSectors.FindAll(i => i.Owner == null && i.systemCount != 0);
        int index = UnityEngine.Random.Range(0, unclaimedTiles.Count);
        Sector startTile = g.capitalSector;
        unclaimedTiles.Remove(startTile);
        g.GainTerritory(startTile);
        for(int i = 0; i < cycles; i++) {
            int territoryCount = g.territory.Count;
            for(int j = 0; j < territoryCount; j++) {
                foreach (Sector n in Array.FindAll(g.territory[j].neighbours, s => s != null)) {
                    if (n.Owner == null && n.systemCount != 0) {
                        g.GainTerritory(n);
                        unclaimedTiles.Remove(n);
                    }
                }
            }
        }
    }
}
