using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company : IHasFunds, IHasOwner, IDisplayed{
    public string name;
    public string slogan;
    public LayeredColoredSprite logo;

    public Magnate founder;
    public TickDate establishedDate;
    public GalaxyEntity headquarteredEntity;

    private Magnate currentOwner;
    public Magnate CurrentOwner {
        get { return currentOwner; }
        set {
            ChangeOwner(value);
        }
    }
    private int funds;
    public int Funds {
        get { return funds; }
        set { funds = value;
            OnFundsChangeEvent?.Invoke(funds);
        }
    }
    public event Action<int> OnFundsChangeEvent;
    public event Action<Magnate> OnOwnerChangeEvent;
    public AIPlayer boardOfDirectors;

    private List<Department> departments = new List<Department>();
    public EngineeringDepartment engineeringDepartment;
    public FinanceDepartment financeDepartment;
    public FacilitiesDepartment facilitiesDepartment;

    public string[] DisplayStrings { get { return new string[1] { name }; } }
    public LayeredColoredSprite[] DisplaySprites { get { return new LayeredColoredSprite[1] { logo }; } }
    public event Action<IDisplayed> DisplayUpdateEvent;

    public Company(Magnate founder) {
        this.founder = founder;
        currentOwner = founder;
        establishedDate = TimeManager.currentTime;


        boardOfDirectors = new AIPlayer();
        boardOfDirectors.FirstName = "Board of Directors";

        TimeManager.instance.OnDayEvent += DailyDepartmentWork;

        engineeringDepartment = new EngineeringDepartment();
        facilitiesDepartment = new FacilitiesDepartment();
        financeDepartment = new FinanceDepartment();
        departments.Add(facilitiesDepartment);
        departments.Add(engineeringDepartment);
        departments.Add(financeDepartment);
    }

    private void DailyDepartmentWork() {
        foreach(Department d in departments) {
            d.DailyWork();
        }
    }

    private void MonthlyDepartmentWork() {
        foreach(Department d in departments) {
            int departmentFunds = financeDepartment.budget[d.departmentType];
            d.MonthlyBudget(departmentFunds);
            Funds -= departmentFunds;
        }
    }

    public Person GetOwner() {
        return CurrentOwner;
    }
    public void ChangeOwner(Magnate newOwner) {
        currentOwner.LoseOwnership(this);
        if(newOwner == null) {
            currentOwner = boardOfDirectors;
        }
        OnOwnerChangeEvent?.Invoke(currentOwner);
    }
}