using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputFieldIncrementFloat : MonoBehaviour {
    public UnityEvent onSubmit = new UnityEvent();
    public Button up;
    public Button down;
    public InputField inputField;
    [SerializeField] //can't serialize properties, so you gotta seralize the private field so we can see it. Setter won't be called from the editor BTW
    private float fieldValue = 1.0f;
    public float incrementAmount = 0.1f;
    public float FieldValue {
        get { return fieldValue; }
        set {
            fieldValue = (float) Math.Round((Decimal) value, sigFigs);
            inputField.text = fieldValue.ToString();
        }
    }
    public float minValue = 0.0f;
    public float maxValue = 100.0f;
    public int sigFigs = 2;

    private void Awake() {
        up.onClick.AddListener(IncrementUp);
        down.onClick.AddListener(IncrementDown);
        inputField.onEndEdit.AddListener(OnValueChanged);
        inputField.text = FieldValue.ToString();
    }

    public void IncrementUp() {
        if (FieldValue < maxValue) {
            FieldValue += incrementAmount;
            FieldValue = Mathf.Min(maxValue, FieldValue);
            FieldValue = (float)Math.Round((Decimal)FieldValue, sigFigs);
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

    public void IncrementDown() {
        if (FieldValue > minValue) {
            FieldValue -= incrementAmount;
            FieldValue = Mathf.Max(minValue, FieldValue);
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

    public void OnValueChanged(string s) {
        Debug.Log(s);
        if (float.TryParse(s, out float temp)) {
            if (temp > maxValue) {
                FieldValue = maxValue;
            } else if (temp < minValue) {
                FieldValue = minValue;
            } else {
                FieldValue = temp;
            }
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

}
