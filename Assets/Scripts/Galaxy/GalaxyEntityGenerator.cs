using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class GalaxyEntityGenerator : MonoBehaviour
{
    public int numberOfMajorPowers = 5;
    public int numberOfMinorPowers = 15;
    public int numberOfSpecies = 20;

    public GalaxyData data;
    public List<SectorData> unoccupiedSectors = new List<SectorData>();
    public GalaxyDisplay previewDisplay;

    public List<Color> entityPallette = new List<Color>();

    private void Awake() {
        for(int i = 0; i < 8; i++) {
            entityPallette.Add(Constants.GetRandomPastelColor());
        }
    }

    public void GenerateEntities() {
        data = GameDataManager.instance.masterGalaxyData;
        string[] speciesNames = MarkovGenerator.GenerateMarkovWord(StringLoader.GetAllStrings("SpeciesButterfly"), numberOfSpecies);
        for(int i = 0; i < numberOfSpecies; i++) {
            GameDataManager.instance.AddNewSpecies(SpeciesGenerator.GetSpecies(speciesNames[i]));
        }

        GameDataManager.instance.ClearAllEntities();
        for(int i =0; i < data.sectors.Length; i++) {
            for(int j=0; j< data.sectors[0].Length; j++) {
                if(data.sectors[i][j].Owner == null && data.sectors[i][j].systemCount > 0) {
                    unoccupiedSectors.Add(data.sectors[i][j]);
                }
            }
        }
        List<List<SectorData>> partitions = GenerateSectorPartitions(unoccupiedSectors);
        string[] entityNames = MarkovGenerator.GenerateMarkovWord(StringLoader.GetAllStrings("StarNames"), partitions.Count);
        for(int i = 0; i < partitions.Count; i ++) {
            float averageSystem = 0;
            float maxSystem = 0;
            float minSystem = 10000;
            float sumOfSquares = 0;
            foreach(SectorData d in partitions[i]) {
                averageSystem += d.systemCount;
                sumOfSquares += d.systemCount * d.systemCount;
                if(d.systemCount > maxSystem) {
                    maxSystem = d.systemCount;
                }
                if(d.systemCount < minSystem) {
                    minSystem = d.systemCount;
                }
            }
            float totalSystem = averageSystem;
            averageSystem = averageSystem / partitions[i].Count;
            float sumOfSquaresAverage = sumOfSquares / partitions[i].Count;
            float standardDeviation = Mathf.Sqrt(sumOfSquaresAverage - (averageSystem * averageSystem));
            Debug.Log("Total System:"+totalSystem+" averageSystem:"+averageSystem+" minSystem:"+minSystem+" maxSystem:"+maxSystem+" stdDeviation:"+standardDeviation);
            GalaxyEntity g = GetEntity(partitions[i]);
            g.name = entityNames[i];
            g.Government = EntityGovernment.GetRandomEntityGovernment();
            Debug.Log(g.Government.leader.FullName + " "+g.Government.governmentName);
            GameDataManager.instance.AddNewEntity(g);
        }

        previewDisplay.ShowTerritory();
    }

    private List<List<SectorData>> GenerateSectorPartitions(List<SectorData> unoccupied) {
        List<List<SectorData>> partitions = new List<List<SectorData>>();
        HashSet<SectorData> unoccupiedHash = new HashSet<SectorData>(unoccupied);
        int maxSize = 25;
        while(unoccupied.Count > 0) {
            HashSet<SectorData> partitionHash = new HashSet<SectorData>();
            List<SectorData> partition = new List<SectorData>();
            int randomID = UnityEngine.Random.Range(0, unoccupied.Count);
            SectorData startPoint = unoccupied[randomID];
            partitionHash.Add(startPoint); //add the first sector
            partition.Add(startPoint);
            unoccupied.RemoveAt(randomID);
            unoccupiedHash.Remove(startPoint);
            while(partitionHash.Count < maxSize) {
                HashSet<SectorData> neighbours = new HashSet<SectorData>();
                foreach(SectorData d in partitionHash) {
                    foreach(SectorData n in d.neighbours) {
                        if (!partitionHash.Contains(n) && unoccupiedHash.Contains(n)) { //add neighbours that are outside of the partition
                            neighbours.Add(n);
                        }
                    }
                }
                if (neighbours.Count > 0) {
                    SectorData p = DistanceWeightedRandomWalk(neighbours, startPoint);
                    partitionHash.Add(p);
                    partition.Add(p);
                    unoccupied.Remove(p);
                    unoccupiedHash.Remove(p);
                } else {
                    break;
                }
            }
            if(partition.Count > 0) {
                partitions.Add(partition);
            }
        }
        return partitions;
    }

    public GalaxyEntity GetEntity(List<SectorData> territory) {
        string[] entityStrings = StringLoader.GetAllStrings("EntityStrings");
        GalaxyEntity g = new GalaxyEntity();
        SectorData capital = territory[UnityEngine.Random.Range(0, territory.Count)];
        territory.Remove(capital);
        g.capitalSector = capital;
        foreach(SectorData sector in territory) {
            g.GainTerritory(sector);
        }
        g.fleetDoctrine = (EntityFleetDoctrine)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EntityFleetDoctrine)).Length);
        g.flag = LayeredSpriteGenerator.GenerateLayeredSprites(new List<string>() { "FlagBack", "FlagMid", "FlagFront" }, entityPallette)[0];
        g.color = g.flag.Colors[0];
        g.adjective = entityStrings[2];
        g.capitalSector.AddGalaxyFeature(new GalaxyFeature(g.fullName + " Capital", GalaxyFeatureType.EntityCapital, g.color));
        TimeManager.SetTimeTrigger(1, g.RequestNewGoals);
        TimeManager.SetTimeTrigger(2400, g.EvaluateGoals);
        return g;
    }

    public SectorData DistanceWeightedRandomWalk(HashSet<SectorData> neighbours, SectorData start) {
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
