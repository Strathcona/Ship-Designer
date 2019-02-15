using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTrigger {
    public Action onEnd;
    public int totalMinutesOfEnd;
    public TickDate startDate;
    public TickDate endDate;

    public TimeTrigger(int minutes, Action _onEnd) {
        onEnd = _onEnd;
        startDate = TimeManager.currentTime;
        totalMinutesOfEnd = TimeManager.totalMinutes + minutes;
        endDate = startDate;
        endDate.Minute += minutes;
    }
}
