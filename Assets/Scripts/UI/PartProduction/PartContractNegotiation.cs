using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartContractNegotiation : MonoBehaviour {
    public PartProduction partProduction;

    public SmallCompanyDisplay companyDisplay;
    public InputFieldIncrement unitsField;
    public InputFieldIncrement timeField;
    public InputFieldIncrement priceField;
    public Company company;
    public Part part;
    public Contract contract;

    public void EnterNegotiations(Company _company, Part _part, Contract _contract) {
        company = _company;
        part = _part;
        contract = _contract;
        companyDisplay.DisplayCompany(company);
        unitsField.FieldValue = partProduction.contract.units;
        timeField.FieldValue = partProduction.contract.time;
        priceField.FieldValue = partProduction.contract.price;
    }

}
