using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyMessage : MonoBehaviour {

    public Company company;
    public Text companyName;
    public Text companyType;
    public Text companyMessage;
    public Image companyLogo;
    public Image CEOImage;
    public Text CEOName;
    public Button button;
    public Button bottomButton;
    public Text bottomButtonText;
    public Image backgroundImage;

    public void ShowBottomButton(bool showBottomButton) {
        bottomButton.gameObject.SetActive(showBottomButton);
    }
    
    public void DisplayCompany(Company c) {
        Clear();
        company = c;
        companyName.text = company.name;
        companyType.text = company.companyType;

        CEOName.text = "CEO: "+company.Owner.FirstName + " " + company.Owner.LastName;
        if(company.logo != null) {
            companyLogo.gameObject.SetActive(true);
        } else {
            companyLogo.gameObject.SetActive(false);
        }
    }

    public void SetSelected(bool selected) {
        if (selected) {
            backgroundImage.color = Color.green;
        } else {
            backgroundImage.color = Color.white;
        }
    }

    public void DisplayCompanyMessage(string s) {
        companyMessage.text = s;
    }

    public void Clear() {
        company = null;
        companyName.text = "";
        companyType.text = "";
        companyMessage.text = "";
        companyLogo.sprite = null;
        CEOImage.sprite = null;
    }

}
