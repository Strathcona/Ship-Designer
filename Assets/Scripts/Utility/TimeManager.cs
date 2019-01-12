using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour {
    public static TimeManager instance;
    public int totalMinutes = 0;
    public int minutes = 0;
    public int hours = 0;
    public int ticks = 0;
    public int tocks = 0;
    public float secondsPerMinute = 0.8f;
    public float baselineSecondsPerMinute = 0.8f;
    public float warpSecondsPerHour = 0.3f;
    public float baselineWarpSecondsPerHour = 0.3f;
    private float littleTimer = 0.0f;
    private float bigTimer = 0.0f;
    public bool paused = false;
    public bool warped = false;

    public static int minutesPerHour = 60;
    public static int hoursPerTick = 24;
    public static int ticksPerTock = 400;

    public List<Action<int>> actionsOnTick = new List<Action<int>>();
    public List<Action> actionsOnMinute =  new List<Action>();
    public List<Timer> timers = new List<Timer>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("You've put another time manager somewhere...");
        }
    }

    public string GetTimeString() {
        if (!warped) {
            return hours.ToString("D2") + ":" + minutes.ToString("D2");
        } else {
            return hours.ToString("D2") + ":" + UnityEngine.Random.Range(0, 6).ToString() + UnityEngine.Random.Range(0, 10).ToString();
        }
    }

    private void Update() {
        if (!paused) {
            if (warped) {
                bigTimer += Time.deltaTime;
                foreach (Action action in actionsOnMinute) {
                    action();
                }

                if (bigTimer > warpSecondsPerHour) {
                    hours += 1;
                    totalMinutes += minutesPerHour;
                    bigTimer -= warpSecondsPerHour;


                    if (hours >= hoursPerTick) {
                        ticks += 1;
                        hours = 0;
                        foreach (Action<int> action in actionsOnTick) {
                            action(ticks);
                        }

                        if (ticks >= ticksPerTock) {
                            tocks += 1;
                            ticks = 0;
                        }
                    }
                }

            } else {
                littleTimer += Time.deltaTime;

                if (littleTimer > secondsPerMinute) {
                    minutes += 1;
                    totalMinutes += 1;
                    foreach (Action action in actionsOnMinute) {
                        action();
                    }
                    littleTimer -= secondsPerMinute;

                    if (minutes >= minutesPerHour) {
                        hours += 1;
                        minutes = 0;

                        if (hours >= hoursPerTick) {
                            ticks += 1;
                            hours = 0;
                            foreach (Action<int> action in actionsOnTick) {
                                action(ticks);
                            }

                            if(ticks >= ticksPerTock) {
                                tocks += 1;
                                ticks = 0;
                            }
                        }
                    }
                }
            }

        }
    }

    public void SetWarp(int warpfactor) {
        if (warpfactor == 0) {
            paused = true;
            warped = false;
        } else if(warpfactor == 1) {
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

    public void SetTimer(int lengthInMinutes, Action onEnd) {
        Timer t = new Timer(lengthInMinutes, onEnd);
    }
}
