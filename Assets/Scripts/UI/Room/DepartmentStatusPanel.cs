using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartmentStatusPanel : MonoBehaviour
{
    public Department department;
    public void DisplayDepartment(Department department) {
        this.department = department;
    }
}
