using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class GalaxyEntity {
    public List<Sector> territory = new List<Sector>();
    public Sector capitalSector;
    public Sprite symbol;
    public int controlledSystems = 0;
    public Color color;
    public NPC leader;
    public string entityName;
    public string leaderTitle;
    public string adjective;
    public string governmentName;

    public Dictionary<ShipType, int> demandForShipType = new Dictionary<ShipType, int>() {
        {ShipType.Battlecruiser,0 },
        {ShipType.Battleship, 0 },
        {ShipType.Carrier, 0 },
        {ShipType.Destroyer, 0 },
        {ShipType.Fighter, 0 },
        {ShipType.Gunboat, 0 },
        {ShipType.HeavyCruiser, 0 },
        {ShipType.LightCruiser, 0 },
        {ShipType.Patrol, 0 },
        {ShipType.Utility, 0 }
    };

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

    public string GetDetailString() {
        string detailString = "";
        detailString += "Sectors: " + territory.Count.ToString() + "\n";
        detailString += "Controlled Systems: " + controlledSystems.ToString();
        return detailString;
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
        return g;
    }
}
