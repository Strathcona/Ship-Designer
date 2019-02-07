using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class GalaxyEntity {
    public List<Coord> Territory;
    public Color color;
    public string name;
    public int ID;

    public GalaxyEntity(int _ID) {
        color = Constants.GetRandomPastelColor();
        ID = _ID;
    }
}
