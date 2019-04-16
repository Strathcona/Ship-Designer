using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SectorData {
    private GalaxyEntity owner;
    public GalaxyEntity Owner {
        get { return owner; }
        set {
            owner = value;
            foreach (SectorData s in Array.FindAll(neighbours, i => i != null)) {
                s.Refresh();
            }
            Refresh();
        }
    }
    public Coord coord;
    public string sectorName = "";
    public int systemCount = 0;
    public SectorData[] neighbours = new SectorData[8] { null, null, null, null, null, null, null, null };
    public List<Action> onRefresh = new List<Action>();
    public float normalizedCount;
    public List<GalaxyFeature> features = new List<GalaxyFeature>();
    public static int[] orthogonal = new int[4] { 1, 3, 4, 6 };
    public static Coord[] neighbourDeltas = new Coord[8] {
        new Coord(-1, -1),
        new Coord(-1, 0),
        new Coord(-1, 1),
        new Coord(0, -1),
        new Coord(0, 1),
        new Coord(1, -1),
        new Coord(1, 0),
        new Coord(1, 1),
    };

    public void Refresh() {
        foreach (Action a in onRefresh) {
            a();
        }
    }

    public void AddGalaxyFeature(GalaxyFeature feature) {
        features.Add(feature);
    }

    public float DistanceTo(SectorData sector) {
        return Mathf.Sqrt(Mathf.Pow(coord.x - sector.coord.x, 2.0f) + Mathf.Pow(coord.y - sector.coord.y, 2.0f));
    }

    public void RemoveOwner() {
        Owner = null;
    }

}
