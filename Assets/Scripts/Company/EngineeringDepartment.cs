﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConstructs;
using System;

public class EngineeringDepartment : Department {
    private int engineeringEffort = 0;
    private List<Part> partDesigns = new List<Part>();
    private Dictionary<IDesigned, int> partAndPriority = new Dictionary<IDesigned, int>();
    public event Action<IDesigned> OnNewIDesign;

    public EngineeringDepartment() {
        departmentType = DepartmentType.Engineering;
    }

    public override void MonthlyBudget(int funds) {
        base.MonthlyBudget(funds);
        engineeringEffort = lastMonthlyBudget;
    }

    public void SubmitPartDesign(Part part) {
        IDesigned design = part as IDesigned;
        partAndPriority.Add(design, 1);
        OnNewIDesign?.Invoke(design);
    }

    public void SubmitShipDesign(Ship ship) {

    }

    public override void DailyWork() {
        int totalPriority = 0;
        foreach(int i in partAndPriority.Values) {
            totalPriority += i;
        }
        float effortPerPriority = (float) engineeringEffort / totalPriority;
        foreach(IDesigned design  in partAndPriority.Keys) {
            design.DesignProgress += partAndPriority[design] * effortPerPriority;
        }
    }

}