using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class Company : IHasFunds, IHasOwner{
    public string name;
    public string companyType;
    public LayeredColoredSprite logo;
    private Player owner;
    public Player Owner {
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
    public event Action<Player> OnOwnerChangeEvent;
    public AIPlayer boardOfDirectors;
    private List<Department> departments = new List<Department>();
    public EngineeringDepartment engineeringDepartment;
    public FinanceDepartment financeDepartment;

    public Company(Player founder) {
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

    public Player GetOwner() {
        return Owner;
    }
    public void ChangeOwner(Player newOwner) {
        owner.LoseOwnership(this);
        if(newOwner == null) {
            owner = boardOfDirectors;
        }
        OnOwnerChangeEvent?.Invoke(owner);
    }
}