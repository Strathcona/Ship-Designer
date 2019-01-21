using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointEditor : MonoBehaviour {
    public List<Hardpoint> hardpoints = new List<Hardpoint>();
    public List<GameObject> hardpointListElements = new List<GameObject>();
    int lastUsedListIndex = 0;
    public GameObject hardpointListPrefab;
    public GameObject hardpointListRoot;

    public InputFieldIncrement size;
    public Toggle externalToggle;
    public Dropdown internalHardpointTypes;
    public Dropdown externalHardpointTypes;
    public PartType newHardpointType;
    public Orientation newHardpointOrientation;
    public OrientationPicker orientationPicker;
    public Button newHardpointButton;

    public Button finishedEditingButton;
    public Button cancelButton;


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

    public void LoadHardpoints(List<Hardpoint> _hardpoints) {
        Clear();
        int neededListElements = _hardpoints.Count - hardpointListElements.Count;
        if (neededListElements > 0) {
            GameObject g = Instantiate(hardpointListPrefab, hardpointListRoot.transform) as GameObject;
            hardpointListElements.Add(g);
            g.SetActive(false);
        }
        foreach (Hardpoint h in _hardpoints) {
            hardpoints.Add(h);
            var passHardpoint = h;
            var passListElement = hardpointListElements[lastUsedListIndex].GetComponent<HardpointListElement>();
            passListElement.DisplayHardpoint(h);
            passListElement.deleteButton.onClick.AddListener(delegate { DeleteHardpoint(passListElement, passHardpoint); });
            hardpointListElements[lastUsedListIndex].SetActive(true);
            lastUsedListIndex += 1;
        }

    }


    public void AddNewHardpoint() {
        Hardpoint h = new Hardpoint(size.FieldValue, newHardpointType, newHardpointOrientation);
        hardpoints.Add(h);
        if(lastUsedListIndex >= hardpointListElements.Count) {
            GameObject g = Instantiate(hardpointListPrefab, hardpointListRoot.transform) as GameObject;
            hardpointListElements.Add(g);
            var passHardpoint = h;
            var passListElement = hardpointListElements[lastUsedListIndex].GetComponent<HardpointListElement>();
            passListElement.DisplayHardpoint(h);
            passListElement.deleteButton.onClick.AddListener(delegate { DeleteHardpoint(passListElement, passHardpoint); });
            lastUsedListIndex += 1;
        }
        
    }

    public void DeleteHardpoint(HardpointListElement listItem, Hardpoint hardpoint) {
        hardpoints.Remove(hardpoint);
        listItem.Clear();
        listItem.gameObject.SetActive(false);
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

    public void Clear() {
        lastUsedListIndex = 0;
        hardpoints.Clear();
        foreach(GameObject g in hardpointListElements) {
            g.GetComponent<HardpointListElement>().Clear();
            g.SetActive(false);
        }
    }
}
