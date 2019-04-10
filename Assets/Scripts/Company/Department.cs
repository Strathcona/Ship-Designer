using UnityEngine;
using System.Collections;
using GameConstructs;
public abstract class Department {
    public Person departmentHead;
    public int staff;
    protected int lastMonthlyBudget;
    public DepartmentType departmentType;

    public abstract void DailyWork();
    public virtual void MonthlyBudget(int funds) {
        lastMonthlyBudget = funds;
    }
}
