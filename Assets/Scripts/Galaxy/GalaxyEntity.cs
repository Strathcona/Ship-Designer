using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class GalaxyEntity {
    public List<GalaxyTile> territory = new List<GalaxyTile>();
    public int controlledSystem = 0;
    public Color color;
    public string name;
    public int ID;

    public GalaxyEntity(int _ID) {
        color = Constants.GetRandomPastelColor();
        ID = _ID;
    }
    public void LoseTerritory(GalaxyTile tile) {
        territory.Remove(tile);
    }

    public void GainTerritory(GalaxyTile tile) {
        if(tile.Owner != null) {
            tile.Owner.LoseTerritory(tile);
        }
        tile.Owner = this;
        territory.Add(tile);
    }
}
