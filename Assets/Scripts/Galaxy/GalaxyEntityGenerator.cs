﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class GalaxyEntityGenerator : MonoBehaviour
{
    public int numberOfMajorPowers = 5;
    public int numberOfMinorPowers = 15;

    public GalaxyData data;
    public List<SectorData> unoccupiedSectors = new List<SectorData>();
    public GalaxyDisplay previewDisplay;

    public void GenerateEntities() {
        data = GameDataManager.instance.masterGalaxyData;
        GameDataManager.instance.ClearAllEntities();
        for(int i =0; i < data.sectors.Length; i++) {
            for(int j=0; j< data.sectors[0].Length; j++) {
                if(data.sectors[i][j].Owner == null && data.sectors[i][j].systemCount > 0) {
                    unoccupiedSectors.Add(data.sectors[i][j]);
                }
            }
        }
        for(int i =0; i < numberOfMajorPowers; i ++) {
            GameDataManager.instance.AddNewEntity(GetEntity(UnityEngine.Random.Range(8, 16)));
        }
        for (int i = 0; i < numberOfMinorPowers; i++) {
            GameDataManager.instance.AddNewEntity(GetEntity(UnityEngine.Random.Range(2,7)));
        }
        previewDisplay.ShowTerritory();
    }

    public GalaxyEntity GetEntity(int size) {
        if(unoccupiedSectors.Count > 0) {
            string[] entityStrings = Constants.GetRandomEntityStrings();
            GalaxyEntity g = new GalaxyEntity();
            SectorData capital = unoccupiedSectors[UnityEngine.Random.Range(0, unoccupiedSectors.Count)];
            unoccupiedSectors.Remove(capital);
            g.capitalSector = capital;
            g.entityName = entityStrings[1];
            g.GainTerritory(capital);
            for (int i = 0; i < size; i++) {
                List<SectorData> neighbours = new List<SectorData>();
                foreach (SectorData d in g.neighbouringSectors) {
                    if (d.systemCount > 0 && d.Owner == null) {
                        neighbours.Add(d);
                    }
                }
                if (neighbours.Count > 0) {
                    SectorData d = DistanceWeightedRandomWalk(neighbours, capital);
                    g.GainTerritory(d);
                    unoccupiedSectors.Remove(d);
                } else {
                    break;
                }
            }

            g.fleetDoctrine = (EntityFleetDoctrine)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EntityFleetDoctrine)).Length);
            g.color = Constants.GetRandomPastelColor();
            g.governmentName = entityStrings[0];
            g.adjective = entityStrings[2];
            g.leaderTitle = entityStrings[3]; g.capitalSector.AddGalaxyFeature(new GalaxyFeature(g.entityName + " Capital", GalaxyFeatureType.EntityCapital, g.color));
            TimeManager.SetTimeTrigger(1, g.RequestNewGoals);
            TimeManager.SetTimeTrigger(2, g.EvaluateGoals);
            Debug.Log("Making Entity " + g.entityName);
            return g;
        } else {
            Debug.Log("Couldn't find unoccupied sector");
            return null;
        }

    }

    public SectorData DistanceWeightedRandomWalk(List<SectorData> neighbours, SectorData start) {
        float distTotal = 0.0f;
        foreach (SectorData s in neighbours) {
            distTotal += 1 / (start.DistanceTo(s) + 1);
        }
        //stochastic choice based on inverse of distance
        float alpha = UnityEngine.Random.Range(0.0f, distTotal);
        float currentDist = 0.0f;
        foreach(SectorData s in neighbours) { 
            currentDist += 1/(start.DistanceTo(s) + 1);
            if(alpha <= currentDist) {
                return s;
            }
        }
        Debug.LogError("You messed up your implementation of a stochastic process dummy. Or neighbours were null. "+neighbours.Count+ "<- is that zero?");
        return null;
    }

}
