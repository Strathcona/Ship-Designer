using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableCompanyMessage : MonoBehaviour {

    public Company company;
    public Text companyName;
    public Text companyType;
    public NPCDisplay npcDisplay;
    public Text companyMessage;
    public Outline outline;
    public Button button;

    public void SetOutline(bool showOutline) {
        outline.enabled = showOutline;
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
