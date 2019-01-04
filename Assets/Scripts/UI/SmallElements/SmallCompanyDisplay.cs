using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallCompanyDisplay : MonoBehaviour {
    
    public Company company;
    public Text companyName;
    public Text companyType;
    public NPCDisplay npcDisplay;

    public void DisplayCompany(Company c) {
        Clear();
        company = c;
        companyName.text = company.name;
        companyType.text = company.companyType;
        npcDisplay.DisplayNPC(company.ceo);
    }


    public void Clear() {
        company = null;
        companyName.text = "";
        companyType.text = "";
        npcDisplay.Clear();
    }

}
