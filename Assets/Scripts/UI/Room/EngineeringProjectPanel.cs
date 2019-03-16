﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class EngineeringProjectPanel : MonoBehaviour {

    public Image progressImage;
    public Text text;
    public IDesigned design;

    private void Start() {

    }

    public void DisplayEngineeringProject(IDesigned design) {
        if(this.design != null) {
            this.design.OnDesignProgressEvent -= UpdateProjectProgress;
        }
        this.design = design;
        Part part = design as Part;
        if(part != null) {
            DisplayPart();
        }
        this.design.OnDesignProgressEvent += UpdateProjectProgress;
    }

    public void DisplayPart() {
        Part part = design as Part;
        text.text = part.GetDescriptionString();
    }

    public void DisplayShip() {

    }

    public void UpdateProjectProgress(IDesigned design) {
        progressImage.fillAmount = 1 - (design.DesignProgress / design.DesignCost);
    }
}
