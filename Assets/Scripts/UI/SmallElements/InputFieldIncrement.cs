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
    [SerializeField] //can't serialize properties, so you gotta seralize the private field so we can see it. Setter won't be called from the editor BTW
    private int fieldValue = 1;
    public int incrementAmount = 1;
    public int FieldValue {
        get { return fieldValue; }
        set {
            fieldValue = value;
            inputField.text = fieldValue.ToString();
        }
    }
    public int minValue = 0;
    public int maxValue = 100;

    private void Awake() {
        up.onClick.AddListener(IncrementUp);
        down.onClick.AddListener(IncrementDown);
        inputField.onEndEdit.AddListener(OnValueChanged);
        inputField.text = FieldValue.ToString();
    }

    public void IncrementUp() {
        if(FieldValue < maxValue) {
            FieldValue += incrementAmount;
            FieldValue = Mathf.Min(maxValue, FieldValue);
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

    public void IncrementDown() {
        if(FieldValue > minValue) {
            FieldValue -= incrementAmount;
            FieldValue = Mathf.Max(minValue, FieldValue);
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

    public void OnValueChanged(string s) {
        if(int.TryParse(s, out int temp)) {
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
