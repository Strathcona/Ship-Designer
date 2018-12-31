using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPopup : MonoBehaviour {
    public Text titleText;
    public Text bodyText;
    public GameObject optionsObject;
    public GameObject optionButtonPrefab;
    public List<GameObject> optionButtons = new List<GameObject>();
    public ModalPopupManager popupManager;

    private void Awake() {
        if(optionButtonPrefab == null) {
            optionButtonPrefab = Resources.Load("Prefabs/Button", typeof(GameObject)) as GameObject;
        }
    }

    public void DisplayMessage(string title, string body, List<string> buttonLabels, List<Action> buttonActions) {
        //if you have fewer actions than labels, extra labels will just close the window

        if(buttonActions.Count > buttonLabels.Count) {
            Debug.LogError("More actions than labels passed to Display Message");
        }
        titleText.text = title;
        bodyText.text = body;

        if(buttonLabels.Count > optionButtons.Count) {
            int neededButtons = buttonLabels.Count - optionButtons.Count;
            for(int i = 0; i < neededButtons; i++) {
                GameObject g = GameObject.Instantiate(optionButtonPrefab);
                g.transform.SetParent(optionsObject.transform);
                optionButtons.Add(g);
                g.SetActive(false);
            }
        }

        for(int j = 0; j < buttonLabels.Count; j++) {
            optionButtons[j].SetActive(true);
            optionButtons[j].GetComponentInChildren<Text>().text = buttonLabels[j];
            optionButtons[j].GetComponent<Button>().onClick.AddListener(delegate { popupManager.ClosePopup(this); });
            if (j < buttonActions.Count) {
                var k = j;//apparently delegates don't pass by value exactly, so copy the value so it won't change
                optionButtons[j].GetComponent<Button>().onClick.AddListener(delegate { buttonActions[k](); });
            }
        }
    }

    private void CloseThis() {
    }

    public void Clear() {
        titleText.text = "";
        bodyText.text = "";
        foreach(GameObject g in optionButtons) {
            g.GetComponentInChildren<Text>().text = "";
            g.GetComponent<Button>().onClick.RemoveAllListeners();
            g.SetActive(false);            
        }
    }
}
