using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyMessage : MonoBehaviour {

    public Company company;
    public Text companyName;
    public Text companyType;
    public NPCDisplay npcDisplay;
    public Text companyMessage;
    public Outline outline;
    public Button button;
    public Button bottomButton;
    public Text bottomButtonText;

    public void ShowOutline(bool showOutline) {
        outline.enabled = showOutline;
    }

    public void ShowBottomButton(bool showBottomButton) {
        bottomButton.gameObject.SetActive(showBottomButton);
    }
    
    public void DisplayCompany(Company c) {
        Clear();
        company = c;
        companyName.text = company.name;
        companyType.text = company.companyType;
        npcDisplay.DisplayNPC(company.ceo);
    }

    public void DisplayCompanyMessage(string s) {
        companyMessage.text = s;
    }

    public void Clear() {
        company = null;
        companyName.text = "";
        companyType.text = "";
        npcDisplay.Clear();
        companyMessage.text = "";
    }

}
