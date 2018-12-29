using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour {
    public Canvas shipDisplayCanvas;
    public PartDesigner partDesigner;
    public Canvas partDesignerCanvas;
    public ShipDisplay shipDisplay;
    public Canvas homescreenCanvas;

    private void Awake() {
        partDesigner = partDesignerCanvas.GetComponent<PartDesigner>();
        shipDisplay = shipDisplayCanvas.GetComponent<ShipDisplay>();
    }

    private void Clear() {
        partDesignerCanvas.gameObject.SetActive(false);
        shipDisplayCanvas.gameObject.SetActive(false);
        homescreenCanvas.gameObject.SetActive(false);
    }

    public void BackToHomescreen() {
        Clear();
        homescreenCanvas.gameObject.SetActive(true);
    }

    public void ShowShipDisplay() {
        Clear();
        shipDisplayCanvas.gameObject.SetActive(true);
    }

    public void ShowPartDesigner() {
        Clear();
        partDesignerCanvas.gameObject.SetActive(true);
        partDesigner.Clear();
    }
}
