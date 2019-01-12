using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer {
    public Action onEnd;
    public int lengthInMinutes;

    public TickDate startDate;
    public TickDate endDate;

    public Timer(int minutes, Action _onEnd) {
        onEnd = _onEnd;
    }

}
