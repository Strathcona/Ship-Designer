using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldMonitor : MonoBehaviour
{
    public InputField inputField;
    private bool currentlyFocused = false;

    private void Awake() {
        inputField = GetComponent<InputField>();
        if (inputField == null) {
            Debug.LogError("Inputfield Monitor has no Input Field");
        }
    }

    private void Update() {
        if (inputField.isFocused && !currentlyFocused) {
            GlobalFlags.fieldFocused = true;
            currentlyFocused = true;
        }
        if(inputField.isFocused != true && currentlyFocused) {
            GlobalFlags.fieldFocused = false;
            currentlyFocused = false;
        }
    }
}
