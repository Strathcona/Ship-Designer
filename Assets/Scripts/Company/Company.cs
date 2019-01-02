using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company {
    public string name;
    public string companyType;
    public NPC ceo;

    public Company() {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        companyType = "Interstellar Conglomerate";
    }

    public CompanyBid GetCompanyBidOnPart(Part p) {
        CompanyBid b = new CompanyBid();
        b.company = this;
        b.proposedRate = Mathf.FloorToInt(p.timeCost * Random.Range(0.1f, 0.2f));
        b.proposedCredits = Mathf.FloorToInt(p.creditCost * Random.Range(0.8f, 1.2f));
        b.maxUnits = Random.Range(20, 200);
        b.maxQuality = Random.Range(0.75f, 1.0f);
        b.minQuality = Random.Range(0.25f, b.maxQuality);
        return b;
    }
}
