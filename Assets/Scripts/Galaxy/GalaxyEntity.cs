using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class GalaxyEntity : IDisplayed {
    public List<SectorData> territory = new List<SectorData>();
    public HashSet<SectorData> neighbouringSectors = new HashSet<SectorData>();
    public SectorData capitalSector;
    public LayeredColoredSprite flag;
    public int controlledSystems = 0;
    public Color color;

    public string name;
    private EntityGovernment government;
    public EntityGovernment Government {
        get { return government; }
    }

    public string FullName { get { return government.governmentName.Replace("*NAME*", name); } }
    public NPC leader { get { return government.rulingParty.leader; } }


    public string[] DisplayStrings {
        get { return new string[1] { FullName }; }
    }
    public LayeredColoredSprite[] DisplaySprites {
        get { return new LayeredColoredSprite[1] { flag }; }
    }
    public event Action<IDisplayed> DisplayUpdateEvent;

    public GalaxyEntity() {
        government = new EntityGovernment("Monarchy");
    }

    public void LoseTerritory(SectorData tile) {
        territory.Remove(tile);
        controlledSystems -= tile.SystemCount;
    }

    public void GainTerritory(SectorData tile) {
        if (tile.Owner != null) {
            tile.Owner.LoseTerritory(tile);
        }
        tile.Owner = this;
        controlledSystems += tile.SystemCount;
        territory.Add(tile);
        RecaluclateNeighboringSectors();
    }

    public void GainTerritory(List<SectorData> data) {
        foreach (SectorData d in data) {
            if (d.Owner != null) {
                d.Owner.LoseTerritory(d);
            }
            d.Owner = this;
            controlledSystems += d.SystemCount;
        }
        territory.AddRange(data);
        RecaluclateNeighboringSectors();
    }

    public void RecaluclateNeighboringSectors() {
        foreach (SectorData s in territory) {
            foreach (SectorData n in Array.FindAll(s.neighbours, i => i != null && i.Owner != this)) {
                neighbouringSectors.Add(n);
            }
        }
    }

    public void ClearEntity() {
        foreach(SectorData d in territory) {
            d.Owner = null;
        }
    }

    public string GetDetailString() {
        string detailString = "";
        detailString += "Sectors: " + territory.Count.ToString() + "\n";
        detailString += "Controlled Systems: " + controlledSystems.ToString();
        return detailString;

    }
}

