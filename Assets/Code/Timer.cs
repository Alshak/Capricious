using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static float timer = 0;
    public Text timerText;
    public static bool timerIsRunning = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timerText != null)
        {
            if (timerIsRunning)
            {
                timer += Time.deltaTime;
            }
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            string secondsString = seconds.ToString();
            if (seconds < 10)
            {
                secondsString = "0" + seconds.ToString();
            }
            timerText.text = minutes.ToString() + ":" + secondsString;
        }
    }

    public static void StartTimer()
    {
        timer = 0;
        timerIsRunning = true;
    }

    public static void PauseTimer()
    {
        timerIsRunning = false;
    }
}
