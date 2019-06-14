using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataManager : MonoBehaviour, IInitialized {
    public static GameDataManager instance;

    private List<GalaxyEntity> entities = new List<GalaxyEntity>();
    public GalaxyEntity[] Entitites { get { return entities.ToArray(); } }

    private List<Company> companies = new List<Company>();
    public Company[] Companies { get { return companies.ToArray(); } }

    private List<Species> species = new List<Species>();
    public Species[] Species { get {return species.ToArray(); } }

    public GalaxyData masterGalaxyData;

    public event Action<Company[]> OnCompaniesChangeEvent;
    public event Action<GalaxyEntity[]> OnEntitiesChangeEvent;

    public void Initialize() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another GameDataManager somewhere");
        }
        masterGalaxyData = new GalaxyData();
    }

    public void ClearAllEntities() {
        foreach(GalaxyEntity e in entities) {
            if(e != null) {
                e.ClearEntity();
            }
        }
        entities.Clear();
    }

    public void ClearAllSpecies() {
        species.Clear();
    }

    public void AddNewEntity(GalaxyEntity entity) {
        entities.Add(entity);
        OnEntitiesChangeEvent?.Invoke(Entitites);
    }

    public void AddNewCompany(Company company) {
        companies.Add(company);
        OnCompaniesChangeEvent?.Invoke(Companies);
    }

    public void AddNewSpecies(Species species) {
        this.species.Add(species);
    }
}
