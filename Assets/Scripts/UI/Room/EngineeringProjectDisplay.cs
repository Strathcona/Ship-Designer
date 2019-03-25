using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringProjectDisplay : MonoBehaviour
{
    public GameObject EngineeringProjectPanel;
    public GameObjectPool EngineeringProjectPanelPool;
    public GameObject EngineeringProjectPanelRoot;
    public GameObject fade;
    public Company playerCompany;
    private bool initialized = false;
    public bool displayInDevelopment = true;
    public bool displayRecentlyDeveloped = true;
    public bool displayAllDeveloped = true;

    private void Start() {
        if (!initialized) {
            EngineeringProjectPanelPool = new GameObjectPool(EngineeringProjectPanel, EngineeringProjectPanelRoot);
            GameDataManager.instance.OnCompaniesChangeEvent += CheckPlayerCompany;
            CheckPlayerCompany(GameDataManager.instance.Companies);
            initialized = true;
        }
    }

    public void CreateNewIDesignPanel(IDesigned design) {
        EngineeringProjectPanel panel = EngineeringProjectPanelPool.GetGameObject().GetComponent<EngineeringProjectPanel>();
        panel.DisplayEngineeringProject(design);
    }


    private void CheckPlayerCompany(Company[] companies) {
        playerCompany = PlayerManager.instance.activePlayer.ActiveCompany;
        if (playerCompany == null) {
            fade.SetActive(true);
        } else {
            fade.SetActive(false);
            if (displayInDevelopment) {
                playerCompany.engineeringDepartment.OnNewIDesign += CreateNewIDesignPanel;
            }
            if (displayRecentlyDeveloped) {
                playerCompany.engineeringDepartment.OnAnyIDesignComplete += CreateNewIDesignPanel;
            }
            if (displayAllDeveloped) {

            }
        }
    }
}
