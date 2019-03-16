using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameConstructs;

public class FinanceDepartment : Department {
    public Dictionary<DepartmentType, int> budget = new Dictionary<DepartmentType, int>();

    public FinanceDepartment() {
        departmentType = DepartmentType.Finance;
    }

    public override void DailyWork() {
    }
}
