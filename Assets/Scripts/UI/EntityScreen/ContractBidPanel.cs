using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractBidPanel : MonoBehaviour {
    public Text titleText;
    public Text unitsText;
    public Text requirementsText;
    public Text criteriaText;
    public Text otherBidsText;
    public Text timeText;
    public Button viewDetailsButton;
    public Button submitButton;
    public ContractBid contractBid;

    public void DisplayContractBid(ContractBid c) {
        Clear();
        contractBid = c;
        titleText.text = contractBid.name;
        unitsText.text = contractBid.units.ToString() + " units";
        string requirements = "";
        foreach(ContractBid.ContractBidRequirement re in contractBid.requirements) {
            requirements += re.requirementSummary + " ";
        }
        requirementsText.text = requirements;
        string criteria = "";
        foreach(ContractBid.ContractBidCriteria cr in contractBid.criteria) {
            criteria += cr.criteriaSummary + " ";
        }
        criteriaText.text = criteria;
        if(contractBid.responses.Count == 0) {
            otherBidsText.text = "No Other Bids";
        } else if (contractBid.responses.Count == 1) {
            otherBidsText.text = "1 Competing Bid";
        } else {
            otherBidsText.text = contractBid.responses.Count.ToString() + " Competing Bids";
        }

    }

    public void Clear() {
        contractBid = null;
        titleText.text = "";
        unitsText.text = "";
        requirementsText.text = "";
        criteriaText.text = "";
        otherBidsText.text = "";
        timeText.text = "";
    }

}
