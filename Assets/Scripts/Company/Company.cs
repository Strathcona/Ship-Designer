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
    public float minimumMargin = 1.1f; //how much margin they must make before they agree
    public float productionSpeed = 1.0f;
    public float changeOrderCost = 1.5f;
    public float miniumQuality = 0.8f;
    public int productionCapacity = 10; //how many units per month the company can produce
    public float prototypeDiscount = 0.5f; //how much of a discount in time and price a company offers a protoType;


    public Company() {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        //get a random company quality
        CompanyQuality quality = (CompanyQuality)System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length));
        companyQualities.Add(quality);
        SetQualityFactors(quality); companyType = Constants.GetCompanyType(companyQualities);
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
        CompanyQuality quality = (CompanyQuality)System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length));
        companyQualities.Add(quality);
        SetQualityFactors(quality);
        companyType = Constants.GetCompanyType(companyQualities);
        partTypes.Add(_partType);
        foreach (PartType pt in (PartType[])System.Enum.GetValues(typeof(PartType))) {
            if (Random.Range(0, 2) == 0) {
                partTypes.Add(pt);
            }
        }
    }

    public void SetQualityFactors(CompanyQuality cq) {
        switch (cq) {
            case CompanyQuality.Cost:
                minimumMargin = 1.01f;
                miniumQuality = 0.5f;
                productionCapacity = 10;
                prototypeDiscount = 0.0f;
                break;
            case CompanyQuality.Ethics:
                minimumMargin = 1.15f;
                productionCapacity = 8;
                break;
            case CompanyQuality.Prestige:
                minimumMargin = 1.15f;
                miniumQuality = 0.85f;
                productionCapacity = 8;
                break;
            case CompanyQuality.Quality:
                minimumMargin = 1.2f;
                productionSpeed = 1.1f;
                productionCapacity = 5;
                miniumQuality = 0.99f;
                break;
            case CompanyQuality.Quantity:
                minimumMargin = 1.07f;
                miniumQuality = 0.75f;
                productionCapacity = 50;
                productionSpeed = 0.9f;
                break;
            case CompanyQuality.Speed:
                productionSpeed = 0.5f;
                miniumQuality = 0.75f;
                break;
        }
    }

    public ContractOpinion GetContractOpinion(Contract contract) {
        ContractOpinion contractOpinion = new ContractOpinion(this, contract);
        string response = "";
        if (!contractOpinion.Willing) { //if we're not willing
            response += "I'm sorry we can't commit to this. ";
            if (contractOpinion.priceTooLow) {
                if (companyQualities.Contains(CompanyQuality.Cost)) {
                    response += "Even our efficent production just won't be profitable at this <color=#888800>price</color>";
                } else {
                    response += "This <color=#888800>price</color> is too low.";
                }
            } else if (contractOpinion.tooManyUnits) {
                if (companyQualities.Contains(CompanyQuality.Quantity)) {
                    response += "Our capacity is great, but even we cannot meet your <color#008800>unit</color> requirements.";
                } else {
                    response += "This exceeds our <color=#888800>production capacity</color>.";
                }
            } else if (contractOpinion.timeTooShort) {
                if (companyQualities.Contains(CompanyQuality.Speed)) {
                    response += "We pride ourselves on our efficency, but we simply cannot meet this delivery <color=#008888>timeframe</color>.";
                } else {
                    response += "I'm afraid we would require more <color=#008888>time</color>.";
                }
            }
        } else { //if we are willing
            if (contractOpinion.prototype) { //if this is for a prototype
                if (companyQualities.Contains(CompanyQuality.Speed)) {
                    response = "We'll get you a design <color=#FFFF00><b>prototype</b></color> for this in no time.";
                }
                if (companyQualities.Contains(CompanyQuality.Quality)) {
                    response = "You'll see even our <color=#FFFF00><b>prototype</b></color> surpass our competition's production line models.";
                }
                if (companyQualities.Contains(CompanyQuality.Cost)) {
                    response = "Tell you what, we'll give you this <color=#FFFF00><b>prototype</b></color> for free.";
                }
                if (companyQualities.Contains(CompanyQuality.Prestige)) {
                    response =  "Your design looks promising, we'd be honored to provide you a <color=#FFFF00><b>prototype</b></color>.";
                }
                if (companyQualities.Contains(CompanyQuality.Ethics)) {
                    response = "This <color=#FFFF00><b>prototype</b></color> looks like it can be sustainably and efficently sourced.";
                }
                if (companyQualities.Contains(CompanyQuality.Quantity)) {
                    response = "We'll get you this <color=#FFFF00><b>prototype</b></color>. Don't worry, we're more than capable of handling further iteration as well.";
                }
            } else { //if it's not a prototype
                response = "I am certain we can meet your contracted requirements";
            }
        }
        contractOpinion.responseString = response;
        return contractOpinion;
    }
}
