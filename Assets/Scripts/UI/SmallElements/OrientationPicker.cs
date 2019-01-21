using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameConstructs;

public class OrientationPicker : MonoBehaviour {
    public Action<Orientation> onChange;
    public Orientation orientation;

    public Toggle fore;
    public Toggle aft;
    public Toggle dorsal;
    public Toggle ventral;
    public Toggle port;
    public Toggle starboard;

    private void Awake() {
        fore.onValueChanged.AddListener(ForeListener);
        aft.onValueChanged.AddListener(AftListener);
        dorsal.onValueChanged.AddListener(DorsalListener);
        ventral.onValueChanged.AddListener(VentralListener);
        port.onValueChanged.AddListener(PortListener);
        starboard.onValueChanged.AddListener(StarboardListener);
    }

    public void ForeListener(bool Set) {
        if (Set) {
            orientation = Orientation.Fore;
            onChange(orientation);
        }
    }

    public void AftListener(bool Set) {
        if (Set) {
            orientation = Orientation.Aft;
            onChange(orientation);
        }
    }
    public void DorsalListener(bool Set) {
        if (Set) {
            orientation = Orientation.Dorsal;
            onChange(orientation);
        }
    }
    public void VentralListener(bool Set) {
        if (Set) {
            orientation = Orientation.Ventral;
            onChange(orientation);
        }
    }
    public void PortListener(bool Set) {
        if (Set) {
            orientation = Orientation.Port;
            onChange(orientation);
        }
    }
    public void StarboardListener(bool Set) {
        if (Set) {
            orientation = Orientation.Starboard;
            onChange(orientation);
        }
    }

}
