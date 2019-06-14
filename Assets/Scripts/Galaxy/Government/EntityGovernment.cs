using UnityEngine;
using System;
using System.Collections.Generic;
using GameConstructs;

public class EntityGovernment {
    public GovernmentParty rulingParty;
    public List<GovernmentParty> allParties;
    public string governmentName;
    public string governmentCatagory;
    public Dictionary<Gender, string> rulerTitles;
    public string viceTitle;
    public string milTitle;
    public string dipTitle;
    public string ecoTitle;
    public int appointmentPeriod; //how often new leaders are appointed. -1 is for life.
    public enum SuccessionMethod { Inheritance};
    public SuccessionMethod successionMethod;

    public EntityGovernment(string type) {
        switch (type) {
            case "Monarchy":
                rulerTitles = new Dictionary<Gender, string>() {
                    {Gender.Male, "Lord" },
                    {Gender.Female, "Lady"},
                    {Gender.Nonbinary, "Their Highness"},
                    {Gender.None,"Their Highness"},
                    {Gender.ThirdGender, "Their Highness"}
                };
                rulingParty = new GovernmentParty();
                rulingParty.leader = new NPC();
                rulingParty.vice = NPC.GetChild(rulingParty.leader);
                rulingParty.milLeader = new NPC();
                rulingParty.ecoLeader = new NPC();
                rulingParty.dipLeader = new NPC();
                rulingParty.leader.Title = rulerTitles[rulingParty.leader.Gender];
                rulingParty.vice.Title = "Heir Apparant";
                rulingParty.milLeader.Title = "Master of Arms";
                rulingParty.dipLeader.Title = "Master of State";
                rulingParty.ecoLeader.Title = "Master of Credit";
                rulingParty.partyName = "House of " + rulingParty.leader.LastName;
                governmentName = "Kingdom of *NAME*";
                governmentCatagory = "Monarchy";
                appointmentPeriod = -1;
                successionMethod = SuccessionMethod.Inheritance;
                break;
        }
    }
}
