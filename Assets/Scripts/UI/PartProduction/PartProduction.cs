using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartProduction : MonoBehaviour
{
    public PartContractProposal partProposals;
    public PartContractNegotiation partNegotiation;

    public Part part;
    public Company company;
    public Contract contract;

    public SelectableFullPartDisplay partDisplay;
    public Button loadPartButton;
    public Button changePartButton;

    private void Awake() {
        partNegotiation.gameObject.SetActive(false);
        partProposals.gameObject.SetActive(false);
        partProposals.partProduction = this;
        partNegotiation.partProduction = this;
    }

    public void AskToLoadPart() {
        PartLoader.instance.LoadPartPopup(LoadPart, label: "Select a Part design to propose to companies.");
    }

    public void AskToChangePart() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Are you sure you want to change which part design you're proposing?",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { AskToLoadPart });
    }

    public void LoadPart(Part p) {
        partNegotiation.gameObject.SetActive(false);
        partProposals.gameObject.SetActive(true);
        part = p;
        partDisplay.DisplayPart(part);
        contract = new Contract(part);
        partProposals.UpdatePart(part);
        loadPartButton.gameObject.SetActive(false);
        changePartButton.gameObject.SetActive(true);
    }

    public void EnterNegotiations() {
        partProposals.gameObject.SetActive(false);
        partNegotiation.gameObject.SetActive(true);
        changePartButton.gameObject.SetActive(false);
        partNegotiation.EnterNegotiations(company, part, contract);
    }

    public void LeaveNegotiations() {
        partNegotiation.gameObject.SetActive(false);
        partProposals.gameObject.SetActive(true);
        changePartButton.gameObject.SetActive(true);
    }


}
