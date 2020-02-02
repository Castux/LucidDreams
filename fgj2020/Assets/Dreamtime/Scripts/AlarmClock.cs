using UnityEngine;
using UnityEngine.UI;
using System;

public class AlarmClock : MonoBehaviour
{
    private Boolean rang = false;
    private Boolean rangTwice = false;

    public void SoundAlarm(float remainingTime)
    {
        GetComponent<AudioSource>().volume += 0.0001f;

        if (!rang)
        {
            GetComponent<AudioSource>().Play();
            rang = true;
        }
       

        if (remainingTime < 5)
        {
            GetComponent<AudioSource>().volume += 0.001f;

            if (!rangTwice)
            {
                GetComponent<AudioSource>().Play();
                rangTwice = true;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
