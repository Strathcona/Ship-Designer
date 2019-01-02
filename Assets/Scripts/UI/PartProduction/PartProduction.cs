using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartProduction : MonoBehaviour {
    public Part part;
    public SelectableFullPartDisplay partDisplay;
    public Button submitBidsButton;
    public GameObjectPool companyDisplayPool;
    public GameObject companyDisplayPrefab;
    public GameObject bidsAttachPoint;
    public SelectableCompanyMessage selectedCompanyBid;

    private void Awake() {
        companyDisplayPrefab = Resources.Load("Prefabs/SelectableCompanyMessage", typeof(GameObject)) as GameObject;
        companyDisplayPool = new GameObjectPool(companyDisplayPrefab, bidsAttachPoint);
    }

    public void AskToLoadPart() {
        PartLoader.instance.LoadPartPopup(LoadPart);
    }

    public void LoadPart(Part p) {
        part = p;
        partDisplay.DisplayPart(part);
    }

    public void SelectBid(SelectableCompanyMessage companyBid) {
        if (selectedCompanyBid != null) {
            selectedCompanyBid.SetOutline(false);
        }
        selectedCompanyBid = companyBid;
        companyBid.SetOutline(true);
    }

    public void SubmitForBids() {
        if (part == null) {
            ModalPopupManager.instance.DisplayModalPopup("Warning", "Please select a part first", "Okay");
        } else {
            foreach (Company c in CompanyLibrary.GetCompanies(3)) {
                GameObject g = companyDisplayPool.GetGameObject();
                SelectableCompanyMessage m = g.GetComponent<SelectableCompanyMessage>();
                m.DisplayCompany(c);
                CompanyBid b = c.GetCompanyBidOnPart(part);
                m.DisplayCompanyMessage(b.GetBidString());
                m.SetOutline(false);
                var val = m;
                m.button.onClick.AddListener(delegate { SelectBid(val); });
            }
            submitBidsButton.gameObject.SetActive(false);
        }
    }
}
