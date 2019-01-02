using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CompanyLibrary {
    public static List<Company> companies;

    static CompanyLibrary() {
        companies = new List<Company>() {
            new Company(),
            new Company(),
            new Company(),
            new Company()
        };
    }

    public static List<Company> GetCompanies(int number) {
        List<Company> c;
        if(number > companies.Count) {
            c = new List<Company>(companies);
            return c;
        }
        c = new List<Company>();
        for(int i = 0; i < number; i++) {
            c.Add(companies[i]);
        }
        return c;
    }
}
