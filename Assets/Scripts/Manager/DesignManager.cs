using UnityEngine;
using System.Collections.Generic;

public class DesignManager : MonoBehaviour, IInitialized {
    public static DesignManager instance;
    private List<Part> PartsInDevelopment = new List<Part>();
    private List<Part> PartDesigns = new List<Part>();
    private List<Ship> ShipDesigns = new List<Ship>();


    public void Initialize() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another Player Manager somewhere");
        }
    }

    public void SubmitPartDesign(Part p) {
        PartsInDevelopment.Add(p);
        PartDesigns.Add(p);
    }

    public Part[] GetAllParts() {
        return PartDesigns.ToArray();
    }

    public Part[] GetDesignedParts() {
        return PartDesigns.FindAll(i => i.IsDesigned == true).ToArray();
    }
}
