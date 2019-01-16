using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer {
    public Action onEnd;
    public int lengthInMinutes;
    public int minutesRemaining;
    public TickDate startDate;
    public TickDate endDate;

    public Timer(int minutes, Action _onEnd) {
        onEnd = _onEnd;
        lengthInMinutes = minutes;
        minutesRemaining = minutes;
        startDate = TimeManager.currentTime;
        endDate = TimeManager.currentTime;
        endDate.Minute += minutes;
    }

    public bool CountDown(int minutesElapsed) {
        minutesRemaining -= minutesElapsed;
        if(minutesRemaining <= 0) {
            onEnd();
            return true;
        } else {
            return false;
        }
    }

}
