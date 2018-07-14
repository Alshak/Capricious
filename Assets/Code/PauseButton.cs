using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PauseButton : MonoBehaviour {
    public GameObject pausePanel;
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("PauseButton"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
