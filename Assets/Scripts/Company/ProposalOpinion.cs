using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProposalOpinion {
    public Part part;
    public Company company;
    public bool Willing {
        get { return !priceTooLow && !timeTooShort && !tooManyUnits; }
    }
    public bool prototype; 
    public int requestedPrice; //what the company is asked to get paid
    public int desiredPrice; //what the company wants to get paid
    public float priceFavorability; //how much the company likes the current price
    public bool priceTooLow;
    public int desiredTime; //how long the company wants to take
    public int requestedTime; //how long the company is asked to take
    public float timeFavorability;
    public bool timeTooShort;
    public int desiredUnits; //how many units the company wants to produce
    public int requestedUnits; //how many units the company is asked to produce
    public float unitsFavorability;
    public bool tooManyUnits;

    public string responseString;

    public ProposalOpinion(Company c, Part p, bool _prototype, int units, int time, int price) {
        part = p;
        company = c;

        prototype = _prototype;
        float prototypeFactor = 1.0f;
        if (prototype) {
            prototypeFactor = company.prototypeDiscount;
        }
        requestedUnits = units;
        requestedTime = time;
        requestedPrice = price;

        desiredUnits = company.productionCapacity;
        desiredTime = Mathf.CeilToInt(part.unitTimeCost * requestedUnits * prototypeFactor *c.productionSpeed);
        desiredPrice = Mathf.CeilToInt(part.unitCost * company.minimumMargin * prototypeFactor);

        unitsFavorability = requestedUnits*0.5f; //you want to produce as close to capacity as possible
        if (requestedUnits > company.productionCapacity) {
            tooManyUnits = true;
            unitsFavorability = 0.0f;
        } else {
            tooManyUnits = false;
        }

        timeFavorability = requestedTime / desiredTime; //you want to deliver as soon as you have the parts done, and not earlier
        if (desiredTime > requestedTime) {
            timeTooShort = true;
            timeFavorability = 0.0f;
        } else {
            timeTooShort = false;
        }

        priceFavorability = requestedPrice / desiredPrice; //you want to at least make your minimum margin
        if (requestedPrice < desiredPrice) {
            priceTooLow = true;
            priceFavorability = 0.0f;
        } else {
            priceTooLow = false;
        }

        Debug.Log(company.name + " on " + part.GetDescriptionString());
        Debug.Log("Requested Price:" + requestedPrice + " Desired: " + desiredPrice + " Favorability: " + priceFavorability);
        Debug.Log("Requested Time:" + requestedTime + " Desired: " + desiredTime + " Favorability: " + timeFavorability);
        Debug.Log("Requested Units:" + requestedUnits + " Desired: " + desiredUnits + " Favorability: " + unitsFavorability);
    }
}
