using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class GalaxyEntity {
    public List<Sector> territory = new List<Sector>();
    public HashSet<Sector> neighbouringSectors = new HashSet<Sector>();
    public Sector capitalSector;
    public Sprite symbol;
    public int controlledSystems = 0;
    public Color color;
    public NPC leader;
    public string entityName;
    public string leaderTitle;
    public string adjective;
    public string governmentName;
    public List<EntityGoal> hashtagEntityGoals = new List<EntityGoal>();

    public void LoseTerritory(Sector tile) {
        territory.Remove(tile);
        controlledSystems -= tile.systemCount;
    }

    public void GainTerritory(Sector tile) {
        if(tile.Owner != null) {
            tile.Owner.LoseTerritory(tile);
        }
        tile.Owner = this;
        controlledSystems += tile.systemCount;
        territory.Add(tile);
    }

    public void RecaluclateNeighboringSectors() {
        foreach(Sector s in territory) {
            foreach(Sector n in Array.FindAll(s.neighbours, i => i != null && i.Owner != this)) {
                neighbouringSectors.Add(n);
            }
        }
    }

    public string GetDetailString() {
        string detailString = "";
        detailString += "Sectors: " + territory.Count.ToString() + "\n";
        detailString += "Controlled Systems: " + controlledSystems.ToString();
        return detailString;
    }

    public void RequestNewGoals() {

    }

    public void RequestNewShips() {

    }

    public static GalaxyEntity GetRandomGalaxyEntity(Sector _capitalSector) {
        string[] entityStrings = Constants.GetRandomEntityStrings();
        GalaxyEntity g = new GalaxyEntity();
        g.capitalSector = _capitalSector;
        g.GainTerritory(_capitalSector);
        g.color = Constants.GetRandomPastelColor();
        g.entityName = entityStrings[1];
        g.governmentName = entityStrings[0];
        g.adjective = entityStrings[2];
        g.leaderTitle = entityStrings[3]; g.capitalSector.AddGalaxyFeature(new GalaxyFeature(g.entityName + " Capital", GalaxyFeatureType.EntityCapital, g.color));
        TimeManager.SetTimeTrigger(1, g.RequestNewGoals);
        return g;
    }
}
