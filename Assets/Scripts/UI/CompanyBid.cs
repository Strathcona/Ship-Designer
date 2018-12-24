using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class CompanyBid {
    Part part;
    public string CompanyName;
    public int proposedTime;
    public int proposedCredits;
    public float minQuality;
    public float maxQuality;
    public int actualQuality;
    public int maxUnits;

    public string GetBidString() {
        return CompanyName + " offers " + proposedCredits + " credits per unit and a manufacture time of " + proposedTime + " for up to " + maxUnits + " units";
    }

    public static CompanyBid GetNewBidOnPart(Part p) {
        CompanyBid b = new CompanyBid();
        b.CompanyName = Constants.GetRandomCompanyName();
        b.proposedTime = Mathf.FloorToInt(p.timeCost * Random.Range(0.8f, 1.2f));
        b.proposedCredits = Mathf.FloorToInt(p.creditCost * Random.Range(0.8f, 1.2f));
        b.maxUnits = Random.Range(20, 100);
        b.maxQuality = Random.Range(0.75f, 1.0f);
        b.minQuality = Random.Range(0.25f, b.maxQuality);

        return b;
    }
}
