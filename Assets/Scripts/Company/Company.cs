﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company {
    public string name;
    public string companyType;
    public Sprite logo;
   //hash sets don't take duplicates which is nice
    public HashSet<CompanyQuality> companyQualities = new HashSet<CompanyQuality>();
    public HashSet<PartType> partTypes = new HashSet<PartType>();
    public NPC ceo;
    public int opinion = 100;
    public float costMod = 1.0f; 
    public float speedMod = 1.0f;
    public Color companyColor1;
    public Color companyColor2;
    public float qualityMod = 1.0f;
    public CompanyTextKey textKey = CompanyTextKey.GenericOne;

    public Company() {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        logo = SpriteLoader.GetSymbolPart("Generic");
        companyColor1 = Constants.GetRandomPastelColor();
        companyColor2 = Constants.GetRandomPastelColor();
        //get a random company quality
        CompanyQuality quality = (CompanyQuality)System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length));
        companyQualities.Add(quality);
        SetQualityFactors(quality); companyType = Constants.GetCompanyType(companyQualities);
        foreach (PartType pt in (PartType[]) System.Enum.GetValues(typeof(PartType))) {
            if(Random.Range(0,2) == 0) {
                partTypes.Add(pt);
            }
        }
    }

    public Company(PartType _partType) {
        ceo = new NPC();
        name = Constants.GetRandomCompanyName();
        logo = SpriteLoader.GetSymbolPart("Generic");
        companyColor1 = Constants.GetRandomPastelColor();
        companyColor2 = Constants.GetRandomPastelColor();
        //get a random company quality
        CompanyQuality quality = (CompanyQuality)System.Enum.GetValues(typeof(CompanyQuality)).GetValue(Random.Range(0, System.Enum.GetValues(typeof(CompanyQuality)).Length));
        companyQualities.Add(quality);
        SetQualityFactors(quality);
        companyType = Constants.GetCompanyType(companyQualities);
        partTypes.Add(_partType);
        foreach (PartType pt in (PartType[])System.Enum.GetValues(typeof(PartType))) {
            if (Random.Range(0, 2) == 0) {
                partTypes.Add(pt);
            }
        }
    }

    public void SetQualityFactors(CompanyQuality cq) {
        switch (cq) {
            case CompanyQuality.Cost:
                costMod = 0.5f; ;
                qualityMod = 0.5f;
                speedMod = 0.9f;
                break;
            case CompanyQuality.Ethics:
                costMod = 1.15f;
                qualityMod = 1.1f;
                break;
            case CompanyQuality.Prestige:
                costMod = 1.3f;
                qualityMod = 1.3f;
                speedMod = 1.3f;
                break;
            case CompanyQuality.Quality:
                qualityMod = 1.5f;
                costMod = 1.75f;
                break;
            case CompanyQuality.Quantity:
                speedMod = 0.75f;
                costMod = 1.2f;
                qualityMod = 0.9f;
                break;
            case CompanyQuality.Speed:
                speedMod = 0.4f;
                costMod = 1.1f;
                qualityMod = 0.6f;
                break;
        }
    }
}
