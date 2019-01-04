using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleableInputFieldIncrement : MonoBehaviour
{
    public bool isToggled;
    public Text label;
    public InputFieldIncrement input;
    public Toggle requiredToggle;
    public GameObject fadePanel;
    public UnityEvent onChange = new UnityEvent();
    

    public void Awake() {
        requiredToggle.onValueChanged.AddListener(SetToggle);
        input.onSubmit.AddListener(ChangeHappened);
    }

    public void SetToggle(bool _isToggled) {
        isToggled = _isToggled;
        if (isToggled) {
            fadePanel.SetActive(false);
        } else {
            fadePanel.SetActive(true);
        }
        ChangeHappened();
    }

    public void ChangeHappened() {
        onChange.Invoke();
    }
}
