using UnityEngine;
using System.Collections;

public class Headquarters : Facility {

    public Headquarters(SectorData data, Company company) : base(data, company) {
        upkeep = 10;
    }
}
