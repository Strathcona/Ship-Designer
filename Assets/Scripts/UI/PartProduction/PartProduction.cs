using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartProduction : MonoBehaviour
{
    public PartContractProposal partProposals;
    public PartContractNegotiation partNegotiation;
    public Part part;
    public SelectableFullPartDisplay partDisplay;
    public Button LoadPartButton;
    public Button ChangePartButton;

    private void Awake() {
        partNegotiation.gameObject.SetActive(false);
        partProposals.gameObject.SetActive(false);
    }

    public void AskToLoadPart() {
        PartLoader.instance.LoadPartPopup(LoadPart, label: "Select a Part design to propose to companies");
    }

    public void AskToChangePart() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Are you sure you want to change which part design you're using? Any proposals or negotiations will end.",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { AskToLoadPart });
    }

    public void LoadPart(Part p) {
        partNegotiation.gameObject.SetActive(false);
        partProposals.gameObject.SetActive(true);
        part = p;
        partProposals.LoadPart(part);
        LoadPartButton.gameObject.SetActive(false);
        ChangePartButton.gameObject.SetActive(true);
    }

    public void EnterNegotiations(Company c) {
        partProposals.gameObject.SetActive(false);
        partNegotiation.gameObject.SetActive(true);
    }


}
