using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickDate {
    private int minute;
    public int Minute {
        get { return minute; }
        set {
            if(value >= TimeManager.minutesPerHour) {
                minute = value % TimeManager.minutesPerHour;
                Hour += value / TimeManager.minutesPerHour;
            } else {
                minute = value;
            }
        }
    }
    private int hour;
    public int Hour {
        get { return hour; }
        set {
            if (value >= TimeManager.hoursPerTick) {
                hour = value % TimeManager.hoursPerTick;
                Tick += value / TimeManager.hoursPerTick;
            } else {
                hour = value;
            }
        }
    }
    private int tick;
    public int Tick {
        get { return tick; }
        set {
            if (value >= TimeManager.ticksPerTock) {
                tick = value % TimeManager.ticksPerTock;
                Tock += value / TimeManager.ticksPerTock;
            } else {
                tick = value;
            }
        }
    }
    public int Tock;

    public TickDate(int _minute, int _hour, int _tick, int _tock) {
        Tock = _tock;
        Tick = _tick;
        Hour = _hour;
        Minute = _minute;
    }

    public TickDate(int minutes) {
        Minute = minutes;
    }

    public string GetTimeString() {
        return Hour.ToString("D2") + ":" + Minute.ToString("D2");
    }

    public string GetDateString() {
        return "Tick "+Tick.ToString("D3");
    }

    public int ToMinutes() {
        int minutesFromHour = Hour * TimeManager.minutesPerHour;
        int minutesFromTick = Tick * TimeManager.minutesPerHour * TimeManager.hoursPerTick;
        int minutesFromTock = Tock * TimeManager.ticksPerTock * TimeManager.minutesPerHour * TimeManager.hoursPerTick;
        return minute + minutesFromHour + minutesFromTick + minutesFromTock;
    }
}
