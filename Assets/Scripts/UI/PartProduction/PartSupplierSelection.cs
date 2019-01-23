using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartSupplierSelection : MonoBehaviour {
    public Part part;

    public SelectableFullPartDisplay partDisplay;
    public Button loadPartButton;
    public Button showCompaniesButton;
    public List<CompanyMessage> companyMessages= new List<CompanyMessage>();
    public List<CompanyMessage> companyMessagesPreOpinion = new List<CompanyMessage>();
    public List<Company> companies = new List<Company>();
    public GameObject companyMessagePrefab;
    public GameObject companyMessageAttachPoint;
    public Text baselineValuesText;

    public Company companyToChat;
    public PartSupplyAgreement partOrderToChat;
    public CompanyChatWindow companyChatWindow;

    private void Awake() {
        companyMessagePrefab = Resources.Load("Prefabs/CompanyMessage", typeof(GameObject)) as GameObject;
        companyChatWindow.gameObject.SetActive(false);
        showCompaniesButton.gameObject.SetActive(false);
    }

    public void AskToLoadPart() {
        if(part != null) {
            ModalPopupManager.instance.DisplayModalPopup("Confirmation",
        "Are you sure you want to change your current Part?",
        new List<string>() { "Yes", "No" },
        new List<System.Action>() { LoadPartPopup });
        } else {
            LoadPartPopup();
        }
    }

    public void LoadPartPopup() {
        PartLoader.instance.LoadPartPopup(LoadPart);
    }
    
    public void LoadPart(Part p) {
        part = p;
        partDisplay.gameObject.SetActive(true);
        partDisplay.DisplayPart(part);
        loadPartButton.gameObject.SetActive(false);
        companyChatWindow.gameObject.SetActive(false);
        showCompaniesButton.gameObject.SetActive(true);
    }

    public void ShowCompanies() {
        showCompaniesButton.gameObject.SetActive(false);
        foreach(CompanyMessage cm in companyMessages) {
            cm.Clear();
            cm.gameObject.SetActive(false);
        }
        List<Company> companiesByPartType = CompanyLibrary.GetCompanies(part.partType);
        int neededCompanyDisplays = companiesByPartType.Count - companyMessages.Count;
        if (neededCompanyDisplays > 0) {
            for (int i = 0; i < neededCompanyDisplays; i++) {
                GameObject g = Instantiate(companyMessagePrefab) as GameObject;
                g.transform.SetParent(companyMessageAttachPoint.transform);
                CompanyMessage m = g.GetComponent<CompanyMessage>();
                companyMessages.Add(m);
            }
        }
        int index = 0;
        foreach (Company c in companiesByPartType) {
            companyMessages[index].gameObject.SetActive(true);
            companyMessages[index].DisplayCompany(c);
            index += 1;
        }
        ResetCompanyOpinion();
        while(companyMessagesPreOpinion.Count > 0) {
            StartCoroutine("ShowRandomResponseToProposal");
        }
    }

    IEnumerator ShowRandomResponseToProposal() {
        CompanyMessage message = companyMessagesPreOpinion[Random.Range(0, companyMessagesPreOpinion.Count)];
        PartSupplyAgreement po = message.company.GetPartSupplyProposal(part);
        companyMessagesPreOpinion.Remove(message);
        message.DisplayCompanyMessage(po.comment);
        message.ShowBottomButton(true);
        message.bottomButtonText.text = "Contact";
        message.bottomButton.onClick.RemoveAllListeners();
        message.bottomButton.onClick.AddListener(delegate { AskToEnterNegotiations(message.company, po); });
        yield return new WaitForSeconds(1.5f);
    }

    public void ResetCompanyOpinion() {
        companyMessagesPreOpinion.Clear();
        foreach(CompanyMessage c in companyMessages) {
            c.DisplayCompanyMessage("Let me think here");
            c.ShowBottomButton(false);
            companyMessagesPreOpinion.Add(c);
        }
        while (companyMessagesPreOpinion.Count > 0) {
            StartCoroutine("ShowRandomResponseToProposal");
        }
    }


    public void AskToEnterNegotiations(Company company, PartSupplyAgreement po) {
        companyToChat = company;
        partOrderToChat = po;
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Do you want to enter negotiations with " + company.name + "?",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { OpenCompanyChat });
    }

    public void OpenCompanyChat() {
        companyChatWindow.gameObject.SetActive(true);
        companyChatWindow.StartChatWith(companyToChat, partOrderToChat, GetResultsOfChat);
    }

    public void CloseCompanyChat() {
        companyChatWindow.gameObject.SetActive(false);
    }

    public void GetResultsOfChat(Company company, PartSupplyAgreement partOrder, bool agree) {
        CloseCompanyChat();
        if (agree) {

        }
    }
}
