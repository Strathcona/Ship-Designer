using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class PartSelector : MonoBehaviour {
    public Button finishedEditingButton;
    public HardpointLayout hardpointLayout;
    public PartIconLayout partIconLayout;

    private void Awake() {
        
    }

    public void LoadHardpoints(List<Hardpoint> hardpoints) {
        hardpointLayout.DisplayHardpoints(hardpoints);

    }

    public void DisplayPartInventory() {
        partIconLayout.DisplayParts(Inventory.partInventory);
        Inventory.partInventory
    }


}
