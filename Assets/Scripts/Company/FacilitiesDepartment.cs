using UnityEngine;
using System.Collections.Generic;
using GameConstructs;

public class FacilitiesDepartment : Department {

    public List<Facility> facilities;
    public FacilitiesDepartment() {
            departmentType = DepartmentType.Facilities;

    }
    public override void DailyWork() {

    }

    public override void MonthlyBudget(int funds) {

    }
}
