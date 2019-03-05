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
        gameObject.SetActive(true);
        part = p;
        Clear();
        companies = GameDataManager.instance.Companies;
        GameObject defaultObject = companyMessagePool.GetGameObject();
        
        foreach (Company c in companies) {
            GameObject g = companyMessagePool.GetGameObject();
            CompanyMessage cm = g.GetComponent<CompanyMessage>();
            cm.DisplayCompany(c);
            string message = "";
            if (c == PlayerManager.instance.activePlayer.ActiveCompany) {
                message = "Manufacture in-house";
            } else {
                message = "Negotiate a better deal from a competitor"; 
            }
            cm.DisplayCompanyMessage(message);
            cm.bottomButtonText.text = "Select";
            cm.bottomButton.onClick.RemoveAllListeners();
            var pass = c;
            cm.bottomButton.onClick.AddListener(delegate { AskToSelectCompany(pass); });

            if (part.Manufacturer == c) {
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
            new List<System.Action>() { delegate { FinishEditingManufacturer(pass); } });
    }

    public void FinishEditingManufacturer(Company c) {
        part.Manufacturer = c;
        gameObject.SetActive(false);
    }

    public void CancelEditingManufacturer() {
        gameObject.SetActive(false);
    }

    public void Clear() {
        companyMessagePool.ReleaseAll();
    }
}
