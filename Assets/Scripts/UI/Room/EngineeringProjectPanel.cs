using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class EngineeringProjectPanel : MonoBehaviour {

    public Image progressImage;
    public Image doneImage;
    public PartImageDisplay partImageDisplay;
    public Text text;
    public IDesigned design;

    private void Start() {

    }

    public void DisplayEngineeringProject(IDesigned design) {
        if(this.design != null) {
            this.design.OnDesignProgressEvent -= UpdateProjectProgress;
            this.design.OnDesignChangeEvent -= MarkAsComplete;
        }
        this.design = design;
        Part part = design as Part;
        if(part != null) {
            DisplayPart();
        }
        this.design.OnDesignProgressEvent += UpdateProjectProgress;
        this.design.OnDesignChangeEvent += MarkAsComplete;

        UpdateProjectProgress(this.design);
        if(this.design.IsDesigned != true) {
            doneImage.gameObject.SetActive(false);
        } else {
            doneImage.gameObject.SetActive(true);
        }
    }

    public void DisplayPart() {
        Part part = design as Part;
        partImageDisplay.DisplayPart(part);
        text.text = part.GetDescriptionString();
    }

    public void DisplayShip() {

    }

    public void UpdateProjectProgress(IDesigned design) {
        progressImage.fillAmount = 1 - (design.DesignProgress / design.DesignCost);
    }

    public void MarkAsComplete(IDesigned design) {
        if (design.IsDesigned) {
            doneImage.gameObject.SetActive(true);
        }
    }
}
