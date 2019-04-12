using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company : IHasFunds, IHasOwner, IDisplayed{
    public string name;
    public string companyType;
    public LayeredColoredSprite logo;
    private Magnate owner;
    public Magnate Owner {
        get { return owner; }
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
    public string[] DisplayStrings { get { return new string[1] { name }; } }
    public LayeredColoredSprite[] DisplaySprites { get { return new LayeredColoredSprite[1] { logo }; } }
    public event Action<IDisplayed> DisplayUpdateEvent;

    public Company(Magnate founder) {
        owner = founder;
        boardOfDirectors = new AIPlayer();
        boardOfDirectors.FirstName = "Board of Directors";
        TimeManager.instance.OnDayEvent += DailyDepartmentWork;
        engineeringDepartment = new EngineeringDepartment();
        financeDepartment = new FinanceDepartment();
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
        return Owner;
    }
    public void ChangeOwner(Magnate newOwner) {
        owner.LoseOwnership(this);
        if(newOwner == null) {
            owner = boardOfDirectors;
        }
        OnOwnerChangeEvent?.Invoke(owner);
    }
}