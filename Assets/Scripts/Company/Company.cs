using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company {
    public string name;
    public string companyType;
   //hash sets don't take duplicates which is nice
    public HashSet<CompanyQuality> companyQualities = new HashSet<CompanyQuality>();
    public HashSet<PartType> partTypes = new HashSet<PartType>();
    public NPC ceo;
    public float minimumTimePerComplexity; //how much time below the minimum time before the company agrees to an offer
    public float minimumMargin = 0.1f; //how much margin they must make before they agree
    public int productionCapacity = 10; //how many units per month the company can produce


    public Company() {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        //get a random company quality
        companyQualities.Add((CompanyQuality) System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length)));
        companyType = Constants.GetCompanyType(companyQualities);
        foreach (PartType pt in (PartType[]) System.Enum.GetValues(typeof(PartType))) {
            if(Random.Range(0,2) == 0) {
                partTypes.Add(pt);
            }
        }
    }

    public Company(PartType _partType) {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        //get a random company quality
        companyQualities.Add((CompanyQuality)System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length)));
        companyType = Constants.GetCompanyType(companyQualities);
        partTypes.Add(_partType);
        foreach (PartType pt in (PartType[])System.Enum.GetValues(typeof(PartType))) {
            if (Random.Range(0, 2) == 0) {
                partTypes.Add(pt);
            }
        }
    }

    public string GetCompanyOpinionOnPartProduction(Part p, bool prototype, int units, int deliveryDate,  int priceLimit) {
        Debug.Log(productionCapacity + " " + units);
        Debug.Log((p.creditCost * (1+ minimumMargin)) + " " + priceLimit);
        Debug.Log(p.timeCost + " " + deliveryDate);
        if (prototype) {
            if (companyQualities.Contains(CompanyQuality.Speed)) {
                return "We'll get you a design <color=#FFFF00><b>prototype</b></color> for this in no time.";
            }
            if (companyQualities.Contains(CompanyQuality.Quality)) {
                return "You'll see even our <color=#FFFF00><b>prototype</b></color> surpass our competition's production line models.";
            }
            if (companyQualities.Contains(CompanyQuality.Cost)) {
                return "Tell you what, we'll give you this <color=#FFFF00><b>prototype</b></color> for free.";
            }
            if (companyQualities.Contains(CompanyQuality.Prestige)) {
                return "Your design looks promising, we'd be honored to provide you a <color=#FFFF00><b>prototype</b></color>.";
            }
            if (companyQualities.Contains(CompanyQuality.Ethics)) {
                return "This <color=#FFFF00><b>prototype</b></color> looks like it can be sustainably and efficently sourced.";
            }
            if (companyQualities.Contains(CompanyQuality.Flexibility)) {
                return "We'll get you this <color=#FFFF00><b>prototype</b></color>. Don't worry, we're more than capable of handling further iteration as well.";
            }
        } else {
            if(units > productionCapacity) {
                return "I'm sorry. We simply don't have the production capacity for this many <color=#660000>units</color>.";
            }
            if (priceLimit > p.creditCost*(1+minimumMargin)) {
                return "We simply can't do this order at this <color=#666600>price</color>.";
            }
            if (deliveryDate > 0 && deliveryDate < p.timeCost) {
                return "This <color=#006600>delivery timeframe</color> is simply unworkable.";
            }
        }

        return "Sure. Yeah. We can do that for you.";
    }
}
