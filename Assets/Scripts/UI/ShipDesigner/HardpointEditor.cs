using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointEditor : MonoBehaviour {
    public List<Hardpoint> hardpoints = new List<Hardpoint>();

    public InputFieldIncrement size;
    public Toggle externalToggle;
    public Dropdown internalHardpointTypes;
    public Dropdown externalHardpointTypes;
    public PartType newHardpointType;
    public Orientation newHardpointOrientation;
    public OrientationPicker orientationPicker;
    public Button newHardpointButton;

    public GameObject hardpointListPrefab;
    public GameObject hardpointListRoot;

    private void Awake() {
        newHardpointButton.onClick.AddListener(AddNewHardpoint);
        externalToggle.onValueChanged.AddListener(ToggleExternal);
        internalHardpointTypes.onValueChanged.AddListener(ChangeInternalPartType);
        externalHardpointTypes.onValueChanged.AddListener(ChangeExternalPartType);
        orientationPicker.onChange = ChangeOrientation;

        internalHardpointTypes.onValueChanged.Invoke(internalHardpointTypes.value);
        externalHardpointTypes.onValueChanged.Invoke(externalHardpointTypes.value);
        ToggleExternal(externalToggle.isOn);
    }

    public void AddNewHardpoint() {
        Hardpoint h = new Hardpoint(size.FieldValue, newHardpointType, newHardpointOrientation);
        hardpoints.Add(h);
        GameObject g = Instantiate(hardpointListPrefab, hardpointListRoot.transform) as GameObject;
        var passObject = g;
        var passHardpoint = h;
        //make the button delete the list item
        g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DeleteHardpoint(passObject, passHardpoint); });
        string hardpointString = "Size " + h.allowableSize.ToString() + " ";
        if(h.orientation == Orientation.Internal) {
            hardpointString += "Internal " + h.allowableType.ToString() + " Mount";
        } else {
            hardpointString += h.orientation.ToString() + " " + h.allowableType.ToString() + " Hardpoint";
        }
        g.transform.GetChild(1).GetComponent<Text>().text = hardpointString;
            
    }

    public void DeleteHardpoint(GameObject listItem, Hardpoint hardpoint) {
        hardpoints.Remove(hardpoint);
        Destroy(listItem);
    }

    public void ToggleExternal(bool isExternal) {
        if (isExternal) {
            externalHardpointTypes.gameObject.SetActive(true);
            orientationPicker.gameObject.SetActive(true);
            internalHardpointTypes.gameObject.SetActive(false);

            orientationPicker.onChange.Invoke(orientationPicker.orientation);
            externalHardpointTypes.onValueChanged.Invoke(externalHardpointTypes.value);
        } else {
            externalHardpointTypes.gameObject.SetActive(false);
            orientationPicker.gameObject.SetActive(false);
            internalHardpointTypes.gameObject.SetActive(true);

            newHardpointOrientation = Orientation.Internal;
            internalHardpointTypes.onValueChanged.Invoke(internalHardpointTypes.value);
        }
    }

    public void ChangeExternalPartType(int value) {
        switch (value) {
            case 0:
                newHardpointType = PartType.Weapon;
                break;
            case 1:
                newHardpointType = PartType.Engine;
                break;
            case 2:
                newHardpointType = PartType.Sensor;
                break;
            default:
                Debug.LogError("Bad part type in external part dropdown in hardpoint editor");
                break;
        }
    }

    public void ChangeInternalPartType(int value) {
        switch (value) {
            case 0:
                newHardpointType = PartType.Reactor;
                break;
            case 1:
                newHardpointType = PartType.FireControl;
                break;
            default:
                Debug.LogError("Bad part type in internal part dropdown in hardpoint editor");
                break;
        }
    }

    public void ChangeOrientation(Orientation orientation) {
        newHardpointOrientation = orientation;
    }
}
