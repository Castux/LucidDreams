using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public float DreamtimeInSeconds = 60f * 3f;
    public Text label;

    private float remainingTime;

    void Start()
    {
        remainingTime = DreamtimeInSeconds;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
        }

        var ts = TimeSpan.FromSeconds(remainingTime).ToString("mm\\:ss");
        label.text = ts;
    }
}
