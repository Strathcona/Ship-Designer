using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyFundsDisplay : MonoBehaviour
{
    public Text text;

    private void Start() {
        if (text == null) {
            text = GetComponent<Text>();
            if (text == null) {
                Debug.LogError("Player Name Display couldn't find Text on " + gameObject.name);
            }
        }
        text.text = "---";
        PlayerManager.instance.activePlayer.OnActiveCompanyChangeEvent += PlayerCompanyChange;
    }

    public void PlayerCompanyChange(Company company) {
        if(company != null) {
            company.OnFundsChangeEvent += UpdateCompanyFunds;
        } else {
            text.text = "---";
        }
    }

    public void UpdateCompanyFunds(int funds) {
        text.text = funds.ToString();
    }
}
