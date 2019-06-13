using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class EntityGovernment {
    public List<NPC> ruler;
    public 
    public string governmentName;
    public int appointmentPeriod; //how often new leaders are appointed. -1 is for life.
    public event Action<EntityGovernment> OnGovernmentAppointmentEvent;
    public GalaxyEntity galaxyEntity;


}
