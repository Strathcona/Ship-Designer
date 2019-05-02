using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    public GameObject cursorObject;
    public GeneralDisplay cursorDisplay;

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            cursorObject.transform.position = hit.transform.position;
            foreach (MonoBehaviour m in hit.transform.gameObject.GetComponents<MonoBehaviour>()) {
                IDisplayed iDisplayed = (IDisplayed) m;
                if (iDisplayed != null) {
                    cursorDisplay.Display(iDisplayed);
                }
            }
        }
    }
}
