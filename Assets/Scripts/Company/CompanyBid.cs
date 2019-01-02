using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class CompanyBid {
    Part part;
    public Company company;
    public int proposedRate;
    public int proposedCredits;
    public float minQuality;
    public float maxQuality;
    public int actualQuality;
    public int maxUnits;

    public string GetBidString() {
        return company.name + " can offer you a price of " + proposedCredits + " credits per unit for a production run of up to " + maxUnits + " units. We'd be able to produce "+proposedRate+" every month.";
    }
}
