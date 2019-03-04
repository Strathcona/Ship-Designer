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
            inputField.text = FieldValue.ToString();
        }
    }
    [SerializeField]
    private int minValue = 0;
    public int MinValue {
        get { return minValue; }
        set { ChangeMinValue(value); }
    }
    [SerializeField]
    private int maxValue = 100;
    public int MaxValue {
        get { return maxValue; }
        set { ChangeMaxValue(value); }
    }

    private void Awake() {
        up.onClick.AddListener(IncrementUp);
        down.onClick.AddListener(IncrementDown);
        inputField.onEndEdit.AddListener(OnValueChanged);
        inputField.text = FieldValue.ToString();
    }

    public void IncrementUp() {
        if(FieldValue < MaxValue) {
            FieldValue = Mathf.Min(MaxValue, FieldValue + incrementAmount);
        }
        onSubmit.Invoke();
    }

    public void IncrementDown() {
        if(FieldValue > MinValue) {
            FieldValue = Mathf.Max(MinValue, FieldValue - incrementAmount);
        }
        onSubmit.Invoke();
    }

    public void ChangeMaxValue(int newMax) {
        if (newMax < minValue) {
            maxValue = minValue;
        } else {
            maxValue = newMax;
        }

        if(FieldValue > maxValue) {
            FieldValue = maxValue;
        }
    }

    public void ChangeMinValue(int newMin) {
        if (newMin > maxValue) {
            minValue = maxValue;
        } else {
            minValue = newMin;
        }

        if (FieldValue < minValue) {
            FieldValue = minValue;
        }
    }

    public void OnValueChanged(string s) {
        if(int.TryParse(s, out int temp)) {
            if (temp > MaxValue) {
                FieldValue = MaxValue;
            } else if (temp < MinValue) {
                FieldValue = MinValue;
            } else {
                FieldValue = temp;
            }
        }
        onSubmit.Invoke();
    }

}
