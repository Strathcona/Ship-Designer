using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SectorData: IDisplayed {
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
    public string SystemCountString {
        get {
            if (systemCount == 0) {
                return "No Known Systems";
            }
            if (systemCount == 1) {
                return "1 System";
            }
            return systemCount.ToString() + " Systems";
        }
    }
    public SectorData[] neighbours = new SectorData[8] { null, null, null, null, null, null, null, null };
    public float normalizedCount;
    public List<GalaxyFeature> features = new List<GalaxyFeature>();
    public event Action<SectorData> SectorDataRefreshEvent;

    public string[] DisplayStrings {
        get {
            if (Owner != null) {
                return new string[] { sectorName, Owner.fullName, SystemCountString };
            } else {
                return new string[] { sectorName, "Unclaimed", SystemCountString };
            }
        }
    }
    public LayeredColoredSprite[] DisplaySprites {
        get {
            if (Owner != null) {
                return new LayeredColoredSprite[] { Owner.flag };
            } else {
                return new LayeredColoredSprite[] { null };
            }
        }
    }
    public event Action<IDisplayed> DisplayUpdateEvent;

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
        SectorDataRefreshEvent?.Invoke(this);
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
