using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeStart;
    public TMPro.TMP_Text textBox;
    void Start()
    {
        int min = Convert.ToInt32(timeStart) / 60;
        int sec = Convert.ToInt32(timeStart) - (min * 60);
        textBox.text = "Temps restant " + min.ToString() + ':' + sec.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        float seconds = Mathf.Round(timeStart);
        int min = Convert.ToInt32(seconds) / 60;
        int sec = Convert.ToInt32(seconds) - (min * 60);
        textBox.text = "Temps restant " + min.ToString() + ':' + sec.ToString();
    }
}
