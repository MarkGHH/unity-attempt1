using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TimeSchedule : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] List<Schedule> _schedule;

    private void Start()
    {
        timeManager.WorldtimeChanged += CheckSchedule;
    }

    private void OnDestroy()
    {
        timeManager.WorldtimeChanged -= CheckSchedule;
    }

    private void CheckSchedule(object sender, TimeSpan newTime)
    {
        var schedule = _schedule.FirstOrDefault(schedule => schedule.Hour == newTime.Hours && schedule.Minute == newTime.Minutes);

        schedule?.action?.Invoke();

    }

    [Serializable]
    private class Schedule
    {
        public int Hour;
        public int Minute;
        public UnityEvent action;
    }
}
