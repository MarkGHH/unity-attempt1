using UnityEngine;
using System;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldtimeChanged;
    [SerializeField] private float dayLength; // In seconds
    private TimeSpan currentTime;
    private float minuteLength => dayLength / WorldTime.MinutesInDay;
    private void Start()
    {
        StartCoroutine(AddMinute());
    }
    private IEnumerator AddMinute()
    {
        currentTime += TimeSpan.FromMinutes(1);
        WorldtimeChanged?.Invoke(this, currentTime);
        yield return new WaitForSeconds(minuteLength);
        StartCoroutine(AddMinute());
    }
}
