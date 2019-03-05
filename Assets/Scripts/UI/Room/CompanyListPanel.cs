using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyListPanel : MonoBehaviour
{
    public LogoDisplay logoDisplay;
    public Text companyName;
    public Button button;
    public Company company;

    public void DisplayCompany(Company c) {
        company = c;
        logoDisplay.DisplayLogo(company.logo);
        companyName.text = company.name;
    }
}
