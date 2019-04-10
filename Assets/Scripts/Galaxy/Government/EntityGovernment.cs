using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class EntityGovernment {
    public NPC leader;
    public int appointmentPeriod; //how often new leaders are appointed. -1 is for life.
    public event Action<EntityGovernment> OnGovernmentAppointmentEvent;
    public GalaxyEntity galaxyEntity;
    public string governmentName;

    public void TransitionToGovernment(GalaxyEntity galaxyEntity) {
        this.galaxyEntity = galaxyEntity;
        TransitionToGovernmentImplementation();
    }

    protected abstract void TransitionToGovernmentImplementation();


    public void AppointNewLeader(NPC leader) {
        this.leader = leader;
        AppointNewLeaderImplementation();
        OnGovernmentAppointmentEvent?.Invoke(this);
    }

    protected abstract void AppointNewLeaderImplementation();

    public static EntityGovernment GetRandomEntityGovernment() {
        EntityGovernment[] governments = new EntityGovernment[] {
            new Monarchy(),
            new Federation(),
            new Empire() 
        };
        EntityGovernment g = governments[UnityEngine.Random.Range(0, governments.Length)];
        g.AppointNewLeader(new NPC());
        return g;
    }
}
