using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartProposals : MonoBehaviour {
    public Part part;
    public SelectableFullPartDisplay partDisplay;
    public Button submitBidsButton;
    public List<CompanyMessage> companyMessages= new List<CompanyMessage>();
    public HashSet<CompanyMessage> companyMessagesPreOpinion = new HashSet<CompanyMessage>();
    public List<Company> companies = new List<Company>();
    public GameObject companyMessagePrefab;
    public GameObject companyMessageAttachPoint;
    public CompanyMessage selectedCompanyMessage;
    public GameObject negotiationsPanel;
    public Toggle prototypeToggle;

    public bool isPrototype;
    public GameObject orderRequirements;
    public ToggleableInputFieldIncrement units;
    public ToggleableInputFieldIncrement deliveryTime;
    public ToggleableInputFieldIncrement unitPrice;

    public float timeTillUpdate = 5.0f;
    public float minTime = 0.5f;
    public float maxTime = 2.5f;
    public float timeSinceLastChange = 0f;

    private void Awake() {
        companyMessagePrefab = Resources.Load("Prefabs/SelectableCompanyMessage", typeof(GameObject)) as GameObject;
        units.onChange.AddListener(ResetCompanyOpinion);
        deliveryTime.onChange.AddListener(ResetCompanyOpinion);
        unitPrice.onChange.AddListener(ResetCompanyOpinion);
        prototypeToggle.onValueChanged.AddListener(TogglePrototype);
    }

    public void LoadPart(Part p) {
        part = p;
        partDisplay.DisplayPart(part);
        ShowCompanies();
        timeSinceLastChange = 0;
        timeTillUpdate = Random.Range(minTime, maxTime);
    }

    public void TogglePrototype(bool toggle) {
        isPrototype = toggle;
        orderRequirements.SetActive(!toggle);
        ResetCompanyOpinion();
    }

    private void Update() {
        timeSinceLastChange += Time.deltaTime;
        if(timeSinceLastChange > timeTillUpdate) {
            if(companyMessagesPreOpinion.Count > 0) {
                ShowRandomCompanyOpinion();
            }
        }
    }

    public void ShowCompanies() {
        foreach(CompanyMessage cm in companyMessages) {
            cm.Clear();
            cm.gameObject.SetActive(false);
        }
        List<Company> companiesByPartType = CompanyLibrary.GetCompanies(part.partType);
        int neededCompanyDisplays = companiesByPartType.Count - companyMessages.Count;
        if (neededCompanyDisplays > 0) {
            Debug.Log(neededCompanyDisplays);
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

    public void ShowRandomCompanyOpinion() {
        CompanyMessage[] array = new CompanyMessage[companyMessagesPreOpinion.Count];
        companyMessagesPreOpinion.CopyTo(array);
        CompanyMessage message = array[Random.Range(0, array.Length)];
        companyMessagesPreOpinion.Remove(message);
        int partUnits = -1;
        int partDelivery = -1;
        int partPrice = -1;
        if (units.isToggled) {
            partUnits = units.input.fieldValue;
        }
        if (deliveryTime.isToggled) {
            partDelivery = deliveryTime.input.fieldValue;
        }
        if (unitPrice.isToggled) {
            partPrice = unitPrice.input.fieldValue;
        }
        message.DisplayCompanyMessage(message.company.GetCompanyOpinionOnPartProduction(part, isPrototype, partUnits, partDelivery, partPrice));
        message.ShowBottomButton(true);
        message.bottomButtonText.text = "Enter Negotiations";
        message.bottomButton.onClick.AddListener(delegate { AskToConfirmCompany(message); });
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

    public void AskToConfirmCompany(CompanyMessage s) {
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Do you want to enter negotiations with " + s.company.name + "?",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { EnterNegotiations });
        selectedCompanyMessage = s;
    }

    public void EnterNegotiations() {

    }
}
