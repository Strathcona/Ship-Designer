using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public static class CompanyLibrary {
    public static HashSet<Company> companies = new HashSet<Company>();

    static CompanyLibrary() {
        foreach (PartType pt in (PartType[])System.Enum.GetValues(typeof(PartType))) {
            companies.Add(new Company(pt));
        }
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

    public static List<Company> GetCompanies(PartType partType) {
        List<Company> cs = new List<Company>();
        foreach(Company c in companies) {
            if (c.partTypes.Contains(partType)) {
                cs.Add(c);
            }
        }
        return cs;
    }
}
