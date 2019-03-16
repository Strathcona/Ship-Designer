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
            if (value >= TimeManager.hoursPerDay) {
                hour = value % TimeManager.hoursPerDay;
                Day += value / TimeManager.hoursPerDay;
            } else {
                hour = value;
            }
        }
    }
    private int day;
    public int Day {
        get { return day; }
        set {
            if (value >= TimeManager.daysPerMonth) {
                day = value % TimeManager.daysPerMonth;
                Month += value / TimeManager.daysPerMonth;
            } else {
                day = value;
            }
        }
    }
    private int month;
    public int Month {
        get { return month; }
        set {
            if (value >= TimeManager.monthsPerYear) {
                month = value % TimeManager.monthsPerYear;
                Year += value / TimeManager.monthsPerYear;
            } else {
                month = value;
            }
        }
    }    
    public int Year;

    public TickDate(int minute, int hour, int day, int month, int year) {
        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
    }

    public TickDate(int minutes) {
        Minute = minutes;
    }

    public string GetTimeString() {
        return Hour.ToString("D2") + ":" + Minute.ToString("D2");
    }

    public string GetDateString() {
        return "Day "+Day.ToString("D2")+", "+Month.ToString("D2");
    }

    public int ToMinutes() {
        int minutesFromHour = Hour * TimeManager.minutesPerHour;
        int minutesFromTick = Day * TimeManager.minutesPerHour * TimeManager.hoursPerDay;
        int minutesFromTock = Month * TimeManager.daysPerMonth * TimeManager.minutesPerHour * TimeManager.hoursPerDay;
        return minute + minutesFromHour + minutesFromTick + minutesFromTock;
    }
}
