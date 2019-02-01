using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour {
    public Canvas shipDisplayCanvas;
    public PartDesigner partDesigner;
    public Canvas partDesignerCanvas;
    public ShipDesigner shipDesigner;
    public Canvas homescreenCanvas;
    public Canvas partProductionCanvas;
    public PartSupplierSelection partProduction;
    public Canvas researchCanvas;

    private void Clear() {
        partDesignerCanvas.gameObject.SetActive(false);
        shipDisplayCanvas.gameObject.SetActive(false);
        partProductionCanvas.gameObject.SetActive(false);
        homescreenCanvas.gameObject.SetActive(false);
        researchCanvas.gameObject.SetActive(false);
    }

    public void BackToHomescreen() {
        Clear();
        homescreenCanvas.gameObject.SetActive(true);
    }

    public void ShowShipDesigner() {
        Clear();
        shipDisplayCanvas.gameObject.SetActive(true);
    }

    public void ShowPartDesigner() {
        Clear();
        partDesignerCanvas.gameObject.SetActive(true);
        partDesigner.Clear();
    }

    public void ShowPartProduction() {
        partProductionCanvas.gameObject.SetActive(true);
    }

    public void ShowResearch() {
        researchCanvas.gameObject.SetActive(true);
    }
}
