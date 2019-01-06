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
    [SerializeField] //can't serialize properties, so you gotta do the private field. Setter won't be called from the editor BTW
    private int fieldValue = 1;
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
            FieldValue += 1;
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

    public void IncrementDown() {
        if(FieldValue > minValue) {
            FieldValue -= 1;
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

    public void OnValueChanged(string s) {
        Debug.Log(s);
        int temp = int.Parse(s);
        if(temp > maxValue) {
            FieldValue = maxValue;
        } else if (temp < minValue) {
            FieldValue = minValue;
        } else {
            FieldValue = temp;
        }
        inputField.text = FieldValue.ToString();
        onSubmit.Invoke();
    }

}
