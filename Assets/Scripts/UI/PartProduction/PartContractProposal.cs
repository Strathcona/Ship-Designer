using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartContractProposal : MonoBehaviour {
    public PartProduction partProduction;

    public SelectableFullPartDisplay partDisplay;
    public Button submitBidsButton;
    public List<CompanyMessage> companyMessages= new List<CompanyMessage>();
    public List<CompanyMessage> companyMessagesPreOpinion = new List<CompanyMessage>();
    public List<Company> companies = new List<Company>();
    public GameObject companyMessagePrefab;
    public GameObject companyMessageAttachPoint;
    public Toggle prototypeToggle;

    public InputFieldIncrement unitsField;
    public InputFieldIncrement timeField;
    public InputFieldIncrement priceField;

    public float timeTillUpdate = 5.0f;
    public float minTime = 0.5f;
    public float maxTime = 2.5f;
    public float timeSinceLastChange = 0f;

    private void Awake() {
        companyMessagePrefab = Resources.Load("Prefabs/CompanyMessage", typeof(GameObject)) as GameObject;
        unitsField.onSubmit.AddListener(ResetCompanyOpinion);
        unitsField.onSubmit.AddListener(UpdateContract);
        timeField.onSubmit.AddListener(ResetCompanyOpinion);
        timeField.onSubmit.AddListener(UpdateContract);
        priceField.onSubmit.AddListener(ResetCompanyOpinion);
        priceField.onSubmit.AddListener(UpdateContract);
        prototypeToggle.onValueChanged.AddListener(TogglePrototype);
    }

    public void UpdatePart(Part p) {
        UpdateContract();
        ShowCompanies();
        timeSinceLastChange = 0;
        timeTillUpdate = Random.Range(minTime, maxTime);
    }

    public void TogglePrototype(bool toggle) {
        UpdateContract();
        ResetCompanyOpinion();
    }

    public void UpdateContract() {
        partProduction.contract.prototype = prototypeToggle.isOn;
        partProduction.contract.units = unitsField.FieldValue;
        partProduction.contract.time = timeField.FieldValue;
        partProduction.contract.price = priceField.FieldValue;
    }


    private void Update() {
        timeSinceLastChange += Time.deltaTime;
        if(timeSinceLastChange > timeTillUpdate) {
            if(companyMessagesPreOpinion.Count > 0) {
                ShowRandomResponseToProposal();
            }
        }
    }

    public void ShowCompanies() {
        foreach(CompanyMessage cm in companyMessages) {
            cm.Clear();
            cm.gameObject.SetActive(false);
        }
        List<Company> companiesByPartType = CompanyLibrary.GetCompanies(partProduction.part.partType);
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
    }

    public void ShowRandomResponseToProposal() {
        CompanyMessage message = companyMessagesPreOpinion[Random.Range(0, companyMessagesPreOpinion.Count)];
        ContractOpinion co = message.company.GetContractOpinion(partProduction.contract);
        companyMessagesPreOpinion.Remove(message);
        message.DisplayCompanyMessage(co.responseString);
        if (co.Willing) {
            message.ShowBottomButton(true);
            message.bottomButtonText.text = "Enter Negotiations";
            message.bottomButton.onClick.RemoveAllListeners();
            message.bottomButton.onClick.AddListener(delegate { AskToEnterNegotiations(message.company); });
        }
        timeSinceLastChange = 0;
        timeTillUpdate = Random.Range(minTime, maxTime);
    }

    public void ResetCompanyOpinion() {
        timeSinceLastChange = 0;
        companyMessagesPreOpinion.Clear();
        foreach(CompanyMessage c in companyMessages) {
            c.DisplayCompanyMessage("Let me think here");
            c.ShowBottomButton(false);
            companyMessagesPreOpinion.Add(c);
        }
    }

    public void AskToEnterNegotiations(Company company) {
        partProduction.company = company;
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Do you want to enter negotiations with " + company.name + "?",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { EnterNegotiations });
    }

    public void EnterNegotiations() {
        partProduction.EnterNegotiations();
    }
}
