using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company {
    public string name;
    public string companyType;
    public List<CompanyQuality> companyQualities = new List<CompanyQuality>(); 
    public NPC ceo;
    public float minimumTimeMod = 1f; //how much time below the minimum time before the company agrees to an offer
    public float minimumCostMod = 1f; //how much cost below the minimum cost before the company agrees to an offer
    public int productionCapacity = 10; //how many units per month the company can produce


    public Company() {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        companyQualities.Add((CompanyQuality) System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length)));
        companyType = Constants.GetCompanyType(companyQualities);
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

    public string GetCompanyOpinionOnPartProduction(Part p, int units=-1, int deliveryDate=-1, int unitsPerMonth=-1, int priceLimit = -1) {
        if(p.complexityCost > 30) {
            return "This design looks complex, but I'm sure we could come to an arrangement";
        } else {
            return name+" would be happy to assist you in production";
        } 
    }

}
