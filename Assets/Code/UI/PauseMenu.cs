using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Code.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isVisible = false;

        void Start()
        {

        }

        void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("PauseButton"))
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    ShowMenu();

                }
                else
                {
                    Time.timeScale = 0;
                    HideMenu();
                }
            }
        }

        private void ShowMenu()
        {

        }

        private void HideMenu()
        {

        }
    }
}
