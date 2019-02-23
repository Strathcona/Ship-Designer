using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScreen : MonoBehaviour {
    public static EntityScreen instance;
    public GalaxyEntity galaxyEntity;
    public GameObject mainScreen;

    public GameObject contractScreen;
    public GameObject contractBidPanelPrefab;
    public GameObjectPool contractBidPanelPool;
    public GameObject contractBidRoot;

    public GameObject conversationScreen;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another entity screen somewhere...");
        }
        contractBidPanelPool = new GameObjectPool(contractBidPanelPrefab, contractBidRoot);
        gameObject.SetActive(false);
    }

    public void DisplayEntity(GalaxyEntity _galaxyEntity) {
        galaxyEntity = _galaxyEntity;
        gameObject.SetActive(true);
    }

    public void Close() {
        galaxyEntity = null;
        gameObject.SetActive(false);
    }

    public void ShowMainScreen() {
        ToggleVisible(false);
        mainScreen.SetActive(true);
    }

    public void ShowContractScreen() {
        ToggleVisible(false);
        contractScreen.SetActive(true);
        foreach(ContractBid bid in galaxyEntity.contractBids) {
            GameObject g = contractBidPanelPool.GetGameObject();
            g.GetComponent<ContractBidPanel>().DisplayContractBid(bid);
        }
    }

    public void ClearContractScreen() {
        contractBidPanelPool.ReleaseAll();
    }

    public void ToggleVisible(bool on) {
        mainScreen.SetActive(on);
        contractScreen.SetActive(on);
        conversationScreen.SetActive(on);
    }

}
