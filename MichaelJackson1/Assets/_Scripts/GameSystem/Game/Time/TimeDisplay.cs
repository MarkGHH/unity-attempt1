using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        timeManager.WorldtimeChanged += OnWorldTimeChanged;
    }

    private void OnDisable()
    {
        timeManager.WorldtimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        text.SetText(newTime.ToString(@"hh\:mm"));
    }
}
