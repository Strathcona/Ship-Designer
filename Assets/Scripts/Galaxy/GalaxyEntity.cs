using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

[System.Serializable]
public class GalaxyEntity {
    public List<SectorData> territory = new List<SectorData>();
    public HashSet<SectorData> neighbouringSectors = new HashSet<SectorData>();
    public SectorData capitalSector;
    public Sprite symbol;
    public int controlledSystems = 0;
    public Color color;
    public NPC leader;
    public string entityName;
    public string leaderTitle;
    public string adjective;
    public string governmentName;
    public EntityFleetDoctrine fleetDoctrine;
    public List<EntityGoal> hashtagEntityGoals = new List<EntityGoal>();
    public List<Ship> navy;
    public List<ContractBid> contractBids = new List<ContractBid>();
    public int desiredNavySize = 0;

    public void LoseTerritory(SectorData tile) {
        territory.Remove(tile);
        controlledSystems -= tile.systemCount;
    }

    public void GainTerritory(SectorData tile) {
        if(tile.Owner != null) {
            tile.Owner.LoseTerritory(tile);
        }
        tile.Owner = this;
        controlledSystems += tile.systemCount;
        territory.Add(tile);
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
        Debug.Log("Galaxy Entity " + entityName + " is evaluating goals");
        EntityGoal bestGoal = null;
        float bestUtility = 0.0f;
        foreach(EntityGoal g in hashtagEntityGoals) {
            float util = g.CalculateUtility(this);
            Debug.Log("Goal " + g.goalName + " has a utility of " + util);
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
        Debug.Log("Galaxy Entity " + entityName + " is requesting new ships");
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
        g.entityName = entityStrings[1];
        g.governmentName = entityStrings[0];
        g.adjective = entityStrings[2];
        g.leaderTitle = entityStrings[3]; g.capitalSector.AddGalaxyFeature(new GalaxyFeature(g.entityName + " Capital", GalaxyFeatureType.EntityCapital, g.color));
        TimeManager.instance.OnMonthEvent += g.EvaluateGoals;
        return g;
    }
}

