using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractOpinion {
    public Part part;
    public Company company;
    public bool Willing {
        get { return !priceTooLow || !timeTooShort || !tooManyUnits; }
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

    public string proposalString;
    public string negotiationString;

    public ContractOpinion(Company c, Contract contract) {
        part = contract.part;
        company = c;

        prototype = contract.prototype;
        float prototypeFactor = 1.0f;
        if (contract.prototype) {
            prototypeFactor = company.prototypeDiscount;
        }
        requestedUnits = contract.units;
        requestedTime = contract.time;
        requestedPrice = contract.price;

        desiredUnits = company.productionCapacity;
        desiredTime = Mathf.CeilToInt(part.unitTimeCost * requestedUnits * prototypeFactor);
        desiredPrice = Mathf.CeilToInt(part.unitCost * company.minimumMargin * prototypeFactor);

        if (requestedUnits > company.productionCapacity) {
            tooManyUnits = true;
            unitsFavorability = 0.0f;
        } else {
            tooManyUnits = false;
            unitsFavorability = 1 + 1 / requestedUnits / (company.productionCapacity + 1); //you want to produce as close to capacity as possible
        }

        if (desiredTime > requestedTime) {
            timeTooShort = true;
            timeFavorability = 0.0f;
        } else {
            timeTooShort = false;
            timeFavorability = requestedTime / desiredTime;
        }

        if (requestedPrice < desiredPrice) {
            priceTooLow = true;
            priceFavorability = 0.0f;
        } else {
            priceTooLow = false;
            priceFavorability = requestedPrice / desiredPrice;
        }

        Debug.Log(company.name+" on "+part.modelName + " time:" + timeFavorability + " price:" + priceFavorability + " units:" + unitsFavorability);

    }
}
