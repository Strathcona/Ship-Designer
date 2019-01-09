using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartContractProposal : MonoBehaviour {
    public Part part;

    public SelectableFullPartDisplay partDisplay;
    public Button loadPartButton;
    public Button changePartButton;
    public Button showCompaniesButton;
    public List<CompanyMessage> companyMessages= new List<CompanyMessage>();
    public List<CompanyMessage> companyMessagesPreOpinion = new List<CompanyMessage>();
    public List<Company> companies = new List<Company>();
    public GameObject companyMessagePrefab;
    public GameObject companyMessageAttachPoint;

    public GameObject orderRequirements;
    public GameObject orderRecomendations;

    public Toggle prototypeToggle;
    public InputFieldIncrement unitsField;
    public InputFieldIncrement timeField;
    public InputFieldIncrement priceField;
    public bool proposedPrototype;
    public int proposedUnits;
    public int proposedTime;
    public int proposedPrice;

    public Text baselineValuesText;

    public Company companyToChat;
    public PartOrder partOrderToChat;
    public CompanyChatWindow companyChatWindow;

    private void Awake() {
        companyMessagePrefab = Resources.Load("Prefabs/CompanyMessage", typeof(GameObject)) as GameObject;
        unitsField.onSubmit.AddListener(ResetCompanyOpinion);
        unitsField.onSubmit.AddListener(UpdateProposal);
        timeField.onSubmit.AddListener(ResetCompanyOpinion);
        timeField.onSubmit.AddListener(UpdateProposal);
        priceField.onSubmit.AddListener(ResetCompanyOpinion);
        priceField.onSubmit.AddListener(UpdateProposal);
        prototypeToggle.onValueChanged.AddListener(TogglePrototype);

        companyChatWindow.gameObject.SetActive(false);
        showCompaniesButton.gameObject.SetActive(false);
        changePartButton.gameObject.SetActive(false);
        orderRecomendations.SetActive(false);
        orderRequirements.SetActive(false);
        prototypeToggle.gameObject.SetActive(false);
    }

    public void AskToLoadPart() {
        PartLoader.instance.LoadPartPopup(LoadPart, label:"Select a Part to propose to Companies");
    }

    public void AskToChangePart() {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Are you sure you want to change your current Part?",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { AskToLoadPart });
    }


    public void LoadPart(Part p) {
        part = p;
        partDisplay.DisplayPart(part);

        UpdateProposal();
        SetBaselineValuesText();

        loadPartButton.gameObject.SetActive(false);
        changePartButton.gameObject.SetActive(true);
        companyChatWindow.gameObject.SetActive(false);
        showCompaniesButton.gameObject.SetActive(true);
        changePartButton.gameObject.SetActive(true);
        orderRecomendations.SetActive(true);
        orderRequirements.SetActive(true);
        prototypeToggle.gameObject.SetActive(true);
    }

    public void TogglePrototype(bool toggle) {
        UpdateProposal();
        ResetCompanyOpinion();
    }

    public void SetBaselineValuesText() {
        baselineValuesText.text = "Baseline Price is "+part.unitPrice.ToString()+". Baseline time is " + part.unitTime.ToString();
    }

    public void ResetToBaselineValues() {
        proposedPrice = part.unitPrice * proposedUnits;
        proposedTime = part.unitTime * proposedUnits;
        priceField.FieldValue = proposedPrice;
        timeField.FieldValue = proposedTime;
    }

    public void UpdateProposal() {
        Debug.Log("Updating Proposal");
        proposedPrototype = prototypeToggle.isOn;
        proposedUnits = unitsField.FieldValue;
        proposedTime = timeField.FieldValue;
        proposedPrice = priceField.FieldValue;
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
        PartOrder po = message.company.GetPartOrderProposal(part, proposedUnits, proposedPrototype);
        companyMessagesPreOpinion.Remove(message);
        message.DisplayCompanyMessage(po.proposalString);
        message.ShowBottomButton(true);
        message.bottomButtonText.text = "Contact";
        message.bottomButton.onClick.RemoveAllListeners();
        message.bottomButton.onClick.AddListener(delegate { AskToEnterNegotiations(message.company, po); });
        yield return new WaitForSeconds(1.5f);
    }

    public void ResetCompanyOpinion() {
        UpdateProposal(); //make sure we're up to date
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

    public void AskToEnterNegotiations(Company company, PartOrder po) {
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

    public void GetResultsOfChat(Company company, PartOrder partOrder, bool agree) {
        CloseCompanyChat();
        if (agree) {

        }
    }
}
