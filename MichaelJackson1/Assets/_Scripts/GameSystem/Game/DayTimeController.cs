using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// Later upgrade to something similar to https://www.youtube.com/watch?v=m9hj9PdO328

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400;

    float time = 25200;
    private int days;
    [SerializeField] float timeScale = 60;

    [SerializeField] Color nightLightColor;
    [SerializeField] Color dayLightColor = Color.white;

    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Light2D globalLight;

    [SerializeField] TextMeshProUGUI timeText;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int wholeHours = (int)Hours;
        int wholeMinutes = (int)Minutes;
        timeText.text = wholeHours.ToString("00") + ":" + wholeMinutes.ToString("00");

        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;


        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
