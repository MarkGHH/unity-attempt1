using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private Light2D _light;
    [SerializeField] private TimeManager worldTime;
    [SerializeField] private Gradient gradient;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        worldTime.WorldtimeChanged += OnWorldTimeChanged;
    }
    private void OnDestroy()
    {
        worldTime.WorldtimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        _light.color = gradient.Evaluate(PercentOfDay(newTime));
    }

    private float PercentOfDay(TimeSpan timeSpan)
    {
        return (float)timeSpan.TotalMinutes % WorldTime.MinutesInDay / WorldTime.MinutesInDay;
    }
}
