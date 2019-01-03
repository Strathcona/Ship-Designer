using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputFieldIncrement : MonoBehaviour {
    public UnityEvent onSubmit = new UnityEvent();
    public Button up;
    public Button down;
    public InputField inputField;
    public int fieldValue = 0;
    public int minValue = 0;
    public int maxValue = 100;

    private void Awake() {
        up.onClick.AddListener(IncrementUp);
        down.onClick.AddListener(IncrementDown);
        inputField.onEndEdit.AddListener(OnValueChanged);
        inputField.text = fieldValue.ToString();
    }

    public void IncrementUp() {
        if(fieldValue < maxValue) {
            fieldValue += 1;
        }
        inputField.text = fieldValue.ToString();
        onSubmit.Invoke();
    }

    public void IncrementDown() {
        if(fieldValue > minValue) {
            fieldValue -= 1;
        }
        inputField.text = fieldValue.ToString();
        onSubmit.Invoke();
    }

    public void OnValueChanged(string s) {
        Debug.Log(s);
        int temp = int.Parse(s);
        if(temp > maxValue) {
            fieldValue = maxValue;
        } else if (temp < minValue) {
            fieldValue = minValue;
        } else {
            fieldValue = temp;
        }
        inputField.text = fieldValue.ToString();
        onSubmit.Invoke();
    }

}
