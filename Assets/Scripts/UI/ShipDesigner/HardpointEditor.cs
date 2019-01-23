using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class HardpointEditor : MonoBehaviour {
    public GameObject hardpointListPrefab;
    public GameObject hardpointListRoot;
    public GameObjectPool hardpointListPool;

    public Dropdown sizeDropdown;
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
        hardpointListPool = new GameObjectPool(hardpointListPrefab, hardpointListRoot);
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
        Debug.Log("Loading hardpoints: "+_hardpoints.Count);
        foreach (Hardpoint h in _hardpoints) {
            GameObject g = hardpointListPool.GetGameObject();
            var passHardpoint = h;
            var passListElement = g.GetComponent<HardpointListElement>();
            passListElement.DisplayHardpoint(h);
            passListElement.deleteButton.onClick.RemoveAllListeners();
            passListElement.deleteButton.onClick.AddListener(delegate { DeleteHardpoint(passListElement, passHardpoint); });
        }
    }

    public List<Hardpoint> GetHardpoints() {
        List<Hardpoint> hardpoints = new List<Hardpoint>();
        foreach(HardpointListElement h in hardpointListPool.GetComponentOfUsedObjects<HardpointListElement>()) {
            hardpoints.Add(h.hardpoint);
        }
        return hardpoints;
    }

    public void AddNewHardpoint() {
        Hardpoint h = new Hardpoint((PartSize) sizeDropdown.value, newHardpointType, newHardpointOrientation);
        GameObject g = hardpointListPool.GetGameObject();
        var passHardpoint = h;
        var passListElement = g.GetComponent<HardpointListElement>();
        passListElement.DisplayHardpoint(h);
        passListElement.deleteButton.onClick.AddListener(delegate { DeleteHardpoint(passListElement, passHardpoint); });
    }

    public void DeleteHardpoint(HardpointListElement listItem, Hardpoint hardpoint) {
        hardpointListPool.ReleaseGameObject(listItem.gameObject);
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
        hardpointListPool.ReleaseAll();
    }
}
