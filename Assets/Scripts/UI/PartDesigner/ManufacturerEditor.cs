using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ManufacturerEditor: MonoBehaviour {

    public PartDesigner partDesigner;
    public GameObject companyMessagePrefab;
    public GameObjectPool companyMessagePool;
    public GameObject companyMessageRoot;
    public Part part;
    public Company company;
    public Company[] companies;

    public void Awake() {
        companyMessagePool = new GameObjectPool(companyMessagePrefab, companyMessageRoot);
    }

    public void LoadPart(Part p) {
        part = p;
        Clear();
        companies = GameDataManager.instance.Companies;
        GameObject defaultObject = companyMessagePool.GetGameObject();
        CompanyMessage defaultMessage = defaultObject.GetComponent<CompanyMessage>();
        defaultMessage.DisplayCompanyMessage("We can always manufacture these parts in house. There'll be no great bonuses, but no additional costs either, and you won't be placed at the mercy of an external supplier");
        defaultMessage.bottomButtonText.text = "Select";
        
        foreach (Company c in companies) {
            GameObject g = companyMessagePool.GetGameObject();
            CompanyMessage cm = g.GetComponent<CompanyMessage>();
            cm.DisplayCompany(c);
            string message = "";
            cm.DisplayCompanyMessage(message);
            cm.bottomButtonText.text = "Select";
            cm.bottomButton.onClick.RemoveAllListeners();
            var pass = c;
            cm.bottomButton.onClick.AddListener(delegate { AskToSelectCompany(pass); });

            if (part.manufacturer == c) {
                cm.SetSelected(true);
            } else {
                cm.SetSelected(false);
            }
        }
    }

    public void AskToSelectCompany(Company c) {
        var pass = c;
        ModalPopupManager.instance.DisplayModalPopup("Confirmation",
            "Are you sure you wish to select " + c.name + "?",
            new List<string>() { "Yes", "No" },
            new List<System.Action>() { delegate { SelectCompany(pass); } });
    }

    public void SelectCompany(Company c) {
        company = c;
        partDesigner.FinishedEditingManufacturer();
    }

    public Company GetCompany() {
        return company;
    }

    public void Clear() {
        companyMessagePool.ReleaseAll();
    }
}
