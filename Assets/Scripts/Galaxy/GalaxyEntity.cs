using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class GalaxyEntity: IDisplayed {
    public List<SectorData> territory = new List<SectorData>();
    public HashSet<SectorData> neighbouringSectors = new HashSet<SectorData>();
    public SectorData capitalSector;
    public LayeredColoredSprite flag;
    public int controlledSystems = 0;
    public Color color;
    public EntityFleetDoctrine fleetDoctrine;
    public List<EntityGoal> hashtagEntityGoals = new List<EntityGoal>();
    public List<Ship> navy;
    public List<ContractBid> contractBids = new List<ContractBid>();
    public int desiredNavySize = 0;

    private EntityGovernment government;
    public EntityGovernment Government {
        get { return government; }
        set {
            government = value;
            value.TransitionToGovernment(this);
            OnGovernmentChangeEvent?.Invoke(this);
            DisplayUpdateEvent?.Invoke(this);
        }
    }

    public string fullName { get { return government?.governmentName; } }
    public NPC leader { get { return government?.leader; } }

    public event Action<GalaxyEntity> OnGovernmentChangeEvent;

    public string name;
    public string adjective;

    public string[] DisplayStrings {
        get { return new string[1] { Government.governmentName }; }
    }
    public LayeredColoredSprite[] DisplaySprites {
        get { return new LayeredColoredSprite[1] { flag }; }
    }
    public event Action<IDisplayed> DisplayUpdateEvent;

    public void LoseTerritory(SectorData tile) {
        territory.Remove(tile);
        controlledSystems -= tile.SystemCount;
    }

    public void GainTerritory(SectorData tile) {
        if(tile.Owner != null) {
            tile.Owner.LoseTerritory(tile);
        }
        tile.Owner = this;
        controlledSystems += tile.SystemCount;
        territory.Add(tile);
        RecaluclateNeighboringSectors();
    }

    public void GainTerritory(List<SectorData> data) {
        foreach(SectorData d in data) {
            if (d.Owner != null) {
                d.Owner.LoseTerritory(d);
            }
            d.Owner = this;
            controlledSystems += d.SystemCount;
        }
        territory.AddRange(data);
        RecaluclateNeighboringSectors();
    }

    public void RecaluclateNeighboringSectors() {
        foreach(SectorData s in territory) {
            foreach(SectorData n in Array.FindAll(s.neighbours, i => i != null && i.Owner != this)) {
                neighbouringSectors.Add(n);
            }
        }
    }

    public string GetDetailString() {
        string detailString = "";
        detailString += "Sectors: " + territory.Count.ToString() + "\n";
        detailString += "Controlled Systems: " + controlledSystems.ToString();
        return detailString;
    }

    public void RequestNewGoals() {
        hashtagEntityGoals.Add(EntityGoal.GetRandomGoal());
    }

    public void EvaluateGoals() {
        RequestNewGoals();
        EntityGoal bestGoal = null;
        float bestUtility = 0.0f;
        foreach(EntityGoal g in hashtagEntityGoals) {
            float util = g.CalculateUtility(this);
            if(util > bestUtility) {
                bestGoal = g;
                bestUtility = util;
            }
        }
        if(bestGoal != null) {
            bestGoal.PerformAction(this);
        }
    }

    public void ClearEntityTerritory() {
        foreach(SectorData d in territory) {
            d.RemoveOwner();
        }
    }

    public void RequestNewShips() {
        Debug.Log("Galaxy Entity " + government.governmentName + " is requesting new ships");
        List<ContractBid.ContractBidRequirement> bidRequirements = new List<ContractBid.ContractBidRequirement>();
        bidRequirements.Add(ContractBid.ContractBidRequirement.ShipTypeRequirement(ShipType.Destroyer));
        List<ContractBid.ContractBidCriteria> bidCriteria = new List<ContractBid.ContractBidCriteria>();
        bidCriteria.Add(ContractBid.ContractBidCriteria.LowMinimumCrewCriteria(3));
        int units = UnityEngine.Random.Range(1, 5);
        ContractBid newContract = new ContractBid(this, units, bidCriteria, bidRequirements, adjective + " Naval Expansion");
        contractBids.Add(newContract);
    }

    public static GalaxyEntity GetRandomGalaxyEntity(SectorData _capitalSector) {
        string[] entityStrings = StringLoader.GetAllStrings("entityStrings");
        GalaxyEntity g = new GalaxyEntity();
        g.fleetDoctrine = (EntityFleetDoctrine) UnityEngine.Random.Range(0, Enum.GetValues(typeof(EntityFleetDoctrine)).Length);
        g.capitalSector = _capitalSector;
        g.GainTerritory(_capitalSector);
        g.color = Constants.GetRandomPastelColor();
        TimeManager.instance.OnMonthEvent += g.EvaluateGoals;
        return g;
    }
}

