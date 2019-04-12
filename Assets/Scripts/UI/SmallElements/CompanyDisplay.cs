using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyDisplay : MonoBehaviour
{
    public GeneralDisplay display;

    private void Start() {
        if (display == null) {
            display = GetComponent<GeneralDisplay>();
            if (display == null) {
                Debug.LogError("Company Display couldn't find Text on " + gameObject.name);
            }
        }
        PlayerManager.instance.activePlayer.OnActiveCompanyChangeEvent += PlayerCompanyChange;
    }

    public void PlayerCompanyChange(Company company) {
        if (company != null) {
            display.Display(company);
        } 
    }
}
