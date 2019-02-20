using UnityEngine;
using System.Collections;
using System;

public class EntityGoal{
    public string goalName;
    public Func<GalaxyEntity, float> CalculateUtility;
    public Action<GalaxyEntity> PerformAction;

    public EntityGoal(Func<GalaxyEntity, float> calculateUtility, Action<GalaxyEntity> performAction) {
        CalculateUtility = calculateUtility;
        PerformAction = performAction;
    }

    public static EntityGoal GetRandomGoal() {
        //for now, always return the "Build up Fleet" Goal

        Func<GalaxyEntity, float> util = delegate (GalaxyEntity e) {
            return 1.0f;
        };
        Action<GalaxyEntity> action = delegate (GalaxyEntity e) {
            e.RequestNewShips();
        };
        EntityGoal eg = new EntityGoal(util, action);
        eg.goalName = "Expand the Navy";
        return eg;
    }
}
