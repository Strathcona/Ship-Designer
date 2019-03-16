using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringPanel : MonoBehaviour
{
    public GameObject EngineeringProjectPanel;
    public GameObjectPool EngineeringProjectPanelPool;
    public GameObject EngineeringProjectPanelRoot;
    public DepartmentStatusPanel statusPanel;
    public GameObject fade;
    public Company playerCompany;

    private void Start() {
        EngineeringProjectPanelPool = new GameObjectPool(EngineeringProjectPanel, EngineeringProjectPanelRoot);
        GameDataManager.instance.OnCompaniesChangeEvent += CheckPlayerCompany;
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
            playerCompany.engineeringDepartment.OnNewIDesign += CreateNewIDesignPanel;
        }
    }

}
