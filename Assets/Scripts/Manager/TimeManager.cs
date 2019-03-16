using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour, IInitialized {
    public static TimeManager instance;
    public static int totalMinutes = 0;
    //individually track these so we can easily see if they change.
    public int minutes = 0;
    public int hours = 0;
    public int days = 0;
    public int months = 0;
    public int years = 0;

    public float secondsPerMinute = 0.8f;
    public float baselineSecondsPerMinute = 0.8f;
    public float warpSecondsPerHour = 0.3f;
    public float baselineWarpSecondsPerHour = 0.3f;
    private float littleTimer = 0.0f;
    private float bigTimer = 0.0f;
    public bool locked = false;
    public bool paused = false;
    public bool warped = false;

    public static int minutesPerHour = 60;
    public static int hoursPerDay = 24;
    public static int daysPerMonth = 30;
    public static int monthsPerYear = 12;

    public event Action OnMonthEvent;
    public event Action OnMinuteEvent;
    public event Action OnDayEvent;

    public static List<TimeTrigger> triggers = new List<TimeTrigger>();

    public static TickDate currentTime;

    public void Initialize() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another time manager somewhere...");
        }
        currentTime = new TickDate(0);
    }

    public string GetCurrentTimeString() {
        if (!warped) {
            return hours.ToString("D2") + ":" + minutes.ToString("D2");
        } else {
            return hours.ToString("D2") + ":" + UnityEngine.Random.Range(0, 6).ToString() + UnityEngine.Random.Range(0, 10).ToString();
        }
    }

    public string GetCurrentDateString() {
        return "Day:" + days.ToString() + ", " + months.ToString();
    }

    private void Update() {
        if (!paused) {
            if (warped) {
                bigTimer += Time.deltaTime;
                DeltaMinute(0);
                if (bigTimer > warpSecondsPerHour) {
                    hours += 1;
                    totalMinutes += minutesPerHour;
                    bigTimer -= warpSecondsPerHour;
                    DeltaMinute(minutesPerHour);
                    if (hours >= hoursPerDay) {
                        days += 1;
                        hours = 0;
                        OnDayEvent?.Invoke();
                        if (days >= daysPerMonth) {
                            OnMonthEvent?.Invoke();
                            months += 1;
                            days = 0;
                            if (months >= monthsPerYear) {
                                years += 1;
                                months = 0;
                            }
                        }
                    }
                }
            } else {
                littleTimer += Time.deltaTime;
                if (littleTimer > secondsPerMinute) {
                    minutes += 1;
                    totalMinutes += 1;
                    littleTimer -= secondsPerMinute;
                    DeltaMinute(1);
                    if (minutes >= minutesPerHour) {
                        hours += 1;
                        minutes = 0;
                        if (hours >= hoursPerDay) {
                            days += 1;
                            hours = 0;
                            OnDayEvent?.Invoke();
                            if(days >= daysPerMonth) {
                                OnMonthEvent?.Invoke();
                                months += 1;
                                days = 0;
                                if (months >= monthsPerYear) {
                                    years += 1;
                                    months = 0;
                                }
                            }
                        }
                    }
                }
            }
            currentTime.Minute = minutes;
            currentTime.Hour = hours;
            currentTime.Day = days;
            currentTime.Month = months;
        }
    }

    private void DeltaMinute(int delta) {
        OnMinuteEvent?.Invoke();

        List<TimeTrigger> done = new List<TimeTrigger>();
        foreach (TimeTrigger t in triggers) {
            if (totalMinutes > t.totalMinutesOfEnd) {
                done.Add(t);
            }
        }
        foreach (TimeTrigger t in done) {
            t.onEnd();
            triggers.Remove(t);
        }
    }

    public void LockPaused(bool paused) {
        if (paused) {
            SetWarp(0);
            locked = true;
        } else {
            locked = false;
        }
    }

    public void SetWarp(int warpfactor) {
        if (!locked) {
            if (warpfactor == 0) {
                paused = true;
                warped = false;
            } else if (warpfactor == 1) {
                secondsPerMinute = baselineSecondsPerMinute;
                paused = false;
                warped = false;
            } else if (warpfactor == 2) {
                secondsPerMinute = baselineSecondsPerMinute / 10;
                paused = false;
                warped = false;
            } else if (warpfactor == 3) {
                warpSecondsPerHour = baselineWarpSecondsPerHour;
                paused = false;
                warped = true;
            } else if (warpfactor > 3) {
                warpSecondsPerHour = baselineWarpSecondsPerHour / 10;
                paused = false;
                warped = true;
            }
        }
    }

    public static TimeTrigger SetTimeTrigger(int lengthInMinutes, Action onEnd) {
        TimeTrigger t = new TimeTrigger(lengthInMinutes, onEnd);
        triggers.Add(t);
        return t;
    }

    public static string GetTimeString(int minutes) {
        TickDate date = new TickDate(minutes);
        if(date.Month == 0) {
            if(date.Day == 0) {
                if(date.Hour == 0) {
                    return date.Minute.ToString() + " minutes";
                }
                return date.Hour.ToString() + " hours and " + date.Minute + " minutes";
            }
            return date.Day.ToString()+" ticks, "+ date.Hour.ToString() + " hours and " + date.Minute + " minutes";
        }
        return date.Month.ToString()+" tocks, "+date.Day.ToString() + " ticks, " + date.Hour.ToString() + " hours and " + date.Minute + " minutes";
    }
}
