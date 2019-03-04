using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public static class CompanyLibrary {
    public static HashSet<Company> companies = new HashSet<Company>();

    static CompanyLibrary() {
        Company weyland = new Company();
    }

    public static List<Company> GetCompanies(int number) {
        List<Company> cs = new List<Company>();
        HashSet<Company> returnPool = new HashSet<Company>(companies);
        for(int i = 0; i < number; i++) {
            Company[] array = new Company[returnPool.Count];
            returnPool.CopyTo(array);
            cs.Add(array[Random.Range(0,array.Length)]);
        }
        return cs;
    }
}
