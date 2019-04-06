using UnityEngine;
using System.Collections;

public class EntityGovernment {
    public enum GovernmentType{Empire, Democracy, Federation }
    public string governmentName;
    public string leaderTitle;

    public static EntityGovernment GetEntityGovernment(GovernmentType type) {
        EntityGovernment g = new EntityGovernment();
        switch ( type){
            case GovernmentType.Empire:
                g.governmentName = "Empire of [NAME]";
                g.leaderTitle = "Emperor if [NAME]";
                return g;
            case GovernmentType.Democracy:
                g.governmentName = "[NAME]";
                g.leaderTitle = "President [NAME]";
                return g;
            case GovernmentType.Federation:
                g.governmentName = "[NAME] Alliance";
                g.leaderTitle = "[NAME]";
                return g;
        }
        g.governmentName = "[NAME] Alliance";
        g.leaderTitle = "[NAME]";
        return g;
    }
}
