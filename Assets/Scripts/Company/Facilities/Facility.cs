using UnityEngine;
using System.Collections;

public class Facility {
    public SectorData sectorData;
    public Company Owner;
    public int upkeep;

    public Facility(SectorData sectorData, Company company) {
        this.sectorData = sectorData;
        this.Owner = company;
    }
}
