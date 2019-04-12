using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartDisplay : MonoBehaviour {
    public Part part;
    PartImageDisplay partImageDisplay;
    Text descriptionText;
    Text statisticsText;
    Button selectionButton;
    GameObject selectionBorder;
    private bool selected;
    public bool Selected {
        get { return selected; }
        set {
            if(value == true && part != null) {
                selected = value;
                OnSelect();
            } else {
                selected = value;
                DeselectPartDisplay();
            }
        }
    }

    public event Action<PartDisplay> OnPartDisplaySelectedEvent;

    private void Awake() {
        for(int i = 0; i < transform.childCount; i++) {
            GameObject g = transform.GetChild(i).gameObject;
            switch (g.name) {
                case "Part Image":
                    partImageDisplay = g.GetComponent<PartImageDisplay>();
                    break;
                case "Description Text":
                    descriptionText = g.GetComponent<Text>();
                    break;
                case "Statistics Text":
                    statisticsText = g.GetComponent<Text>();
                    break;
                case "Selection Button":
                    selectionButton = g.GetComponent<Button>();
                    break;
                case "Selection Border":
                    selectionBorder = g;
                    g.SetActive(false);
                    break;
            }
        }
        if (selectionButton != null) {
            selectionButton.onClick.AddListener(OnSelect);
        }
    }
    public void OnSelect() {
        if(part != null) {
            if (Selected) {
                DeselectPartDisplay();
            } else {
                selected = true;
                OnPartDisplaySelectedEvent?.Invoke(this);
                selectionBorder?.SetActive(true);
            }
        }
    }
    public void DeselectPartDisplay() {
        selected = false;
        selectionBorder?.SetActive(false);
    }

    public void DisplayPart(Part p) {
        if(part != null) {
            part.OnPartChangeEvent -= UpdatePartDisplay;
        }
        part = p;
        part.OnPartChangeEvent += UpdatePartDisplay;
        if(partImageDisplay != null) {
            partImageDisplay.DisplayPart(part);
        }
        if (descriptionText != null) {
            descriptionText.text = part.GetDescriptionString();
        }
        if( statisticsText != null) {
            statisticsText.text = p.GetStatisticsString();
        }
    }

    public void UpdatePartDisplay(Part p) {
        if (partImageDisplay != null) {
            partImageDisplay.DisplayPart(part);
        }
        if (descriptionText != null) {
            descriptionText.text = part.GetDescriptionString();
        }
        if (statisticsText != null) {
            statisticsText.text = p.GetStatisticsString();
        }
    }
}
