using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class ModalPopupManager : MonoBehaviour {
    public static ModalPopupManager instance;
    public GameObject modalPopupPrefab;
    public List<GameObject> activePopups = new List<GameObject>();
    public List<GameObject> inactivePopups = new List<GameObject>();

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another modal popup manager somewhere...");
        }

        if (modalPopupPrefab == null) {
            modalPopupPrefab = Resources.Load("Prefabs/Modal Popup", typeof(GameObject)) as GameObject;
        }
    }

    public void DisplayModalPopup(string title, string body, List<string> buttonLabels, List<Action> buttonPopups) {
        if(inactivePopups.Count < 1) {
            GameObject g = GameObject.Instantiate(modalPopupPrefab, transform, true);
            inactivePopups.Add(g);
            RectTransform t = g.GetComponent<RectTransform>();
            t.offsetMax = Vector2.zero;
            t.offsetMin = Vector2.zero;
            t.anchorMin = new Vector2(0.3f, 0.3f);
            t.anchorMax = new Vector2(0.7f, 0.7f);
            g.GetComponent<ModalPopup>().popupManager = this;
            g.SetActive(false);
        }

        GameObject popup = inactivePopups[0];
        activePopups.Add(popup);
        inactivePopups.Remove(popup);
        popup.SetActive(true);
        popup.GetComponent<ModalPopup>().DisplayMessage(title, body, buttonLabels, buttonPopups);
    }

    public void ClosePopup(ModalPopup popup) {
        popup.Clear();
        inactivePopups.Add(popup.gameObject);
        activePopups.Remove(popup.gameObject);
        popup.gameObject.SetActive(false);
    }
}
