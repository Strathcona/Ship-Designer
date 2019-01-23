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
    public int opinion = 100;
    public float minimumMargin = 1.1f; //how much margin they must make before they agree
    public float productionSpeed = 1.0f;
    public float changeOrderCost = 1.5f;
    public float miniumQuality = 0.8f;
    public int productionCapacity = 10; //how many units per month the company can produce
    public float prototypeDiscount = 0.5f; //how much of a discount in time and price a company offers a protoType;
    public List<PartSupplyAgreement> partOrders = new List<PartSupplyAgreement>();
    public CompanyTextKey textKey = CompanyTextKey.GenericOne;

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

    public PartSupplyAgreement GetPartSupplyProposal(Part p) {
        int price = Mathf.CeilToInt(p.unitPrice*minimumMargin*Random.Range(1f,1.1f));
        int time = Mathf.CeilToInt(p.unitTime * productionSpeed * Random.Range(0.8f, 1f));
        PartSupplyAgreement po = new PartSupplyAgreement(p, price, time);
        string proposal = KeyString.GetStringFromKey("PartOrderSimple")+ "Our unit price is <color=#008888>"+price.ToString()+"</color>. At current production capabilities, each unit will take <color=#998800>"+time.ToString()+"</color> ticks for delivery.";
        po.comment = proposal;
        return po;
    }
}
