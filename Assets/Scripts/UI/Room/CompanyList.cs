using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyList : MonoBehaviour
{
    public GameObject companyListPanelPrefab;
    public GameObjectPool companyListPanelPool;
    public GameObject companyListRoot;
    public GameObject foundNewCompanyButton;
    public Company playerCompany;

    private void Start() {
        companyListPanelPool = new GameObjectPool(companyListPanelPrefab, companyListRoot);
        GameDataManager.instance.OnCompaniesChangeEvent += UpdateCompanyList;
        UpdateCompanyList(GameDataManager.instance.Companies);
    }

    public void UpdateCompanyList(Company[] companies) {
        CheckPlayerCompany();
        companyListPanelPool.ReleaseAll();
        foreach(Company c in companies) {
            GameObject g = companyListPanelPool.GetGameObject();
            g.GetComponent<CompanyListPanel>().DisplayCompany(c);
        }
    }

    private void CheckPlayerCompany() {
        playerCompany = PlayerManager.instance.activePlayer.ActiveCompany;
        if (playerCompany == null) {
            foundNewCompanyButton.SetActive(true);
        } else {
            foundNewCompanyButton.SetActive(false);
        }
    }
}
