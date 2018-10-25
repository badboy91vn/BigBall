using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{

    public TextMeshProUGUI txtTimer;
    public float timeStamp;
    public bool usingTimer = false;

    // Use this for initialization
    void Start()
    {
        //SetTimer(timeStamp);
    }

    // Update is called once per frame
    void Update()
    {
        if (usingTimer) SetUIText();
    }

    public void StartTimer()
    {
        SetTimer(timeStamp);
    }

    void SetTimer(float time)
    {
        if (usingTimer) return;
        timeStamp = Time.time + time;

        usingTimer = true;
    }

    void SetUIText()
    {
        float timeLeft = timeStamp - Time.time;

        if (timeLeft < 0)
        {
            txtTimer.text = "00:00";
            usingTimer = false;

            GameManager.Instance.GameOver();

            return;
        }
        float hours, min, sec, minisec;
        GetTimeValues(timeLeft, out hours, out min, out sec, out minisec);

        //print(hours + ":" + min + ":" + sec + ":" + minisec);
        if (hours > 0)
        {
            string hourFormat = GetFormatTime(hours, "{0}");
            string minFormat = GetFormatTime(min, "{1}");
            txtTimer.text = string.Format(hourFormat + ":" + minFormat, hours, min);
        }
        else if (min > 0)
        {
            string minFormat = GetFormatTime(min, "{0}");
            string secFormat = GetFormatTime(sec, "{1}");
            txtTimer.text = string.Format(minFormat + ":" + secFormat, min, sec);
        }
        else
        {
            string secFormat = GetFormatTime(sec, "{0}");
            string minSecFormat = GetFormatTime(minisec, "{1}");
            txtTimer.text = string.Format(secFormat + ":" + minSecFormat, sec, minisec);
        }
    }

    void GetTimeValues(float time, out float hours, out float min, out float sec, out float minisec)
    {
        hours = (int)(time / 3600f);
        min = (int)((time - hours * 3600) / 60f);
        sec = (int)(time - hours * 3600 - min * 60);
        minisec = (int)((time - hours * 3600 - min * 60 - sec) * 100);
    }

    string GetFormatTime(float num, string index)
    {
        string numFormat = index;
        if (num > 0 && num < 10) numFormat = "0" + numFormat;

        return numFormat;
    }
}
