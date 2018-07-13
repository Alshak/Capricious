using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Intro
{
    public class IntroControls : MonoBehaviour
    {
        private bool isController = false;
        private ControlButton[] controlButtons;
        void Start()
        {
            enabled = false;
            controlButtons = GetComponentsInChildren<ControlButton>();
            CheckForController();
        }

        public void CheckForController()
        {
            bool prevController = isController;
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                if (!string.IsNullOrEmpty(Input.GetJoystickNames()[i]))
                {
                    Debug.Log("Joystick Connected");
                    i = Input.GetJoystickNames().Length;
                    
                    isController = true;
                }
                else
                {
                    Debug.Log("Joystick Disconnected");
                    i = Input.GetJoystickNames().Length;
                    isController = false;
                }
            }

            if (prevController != isController)
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            foreach (var child in controlButtons)
            {
                child.RefreshKeyImage(isController);
            }
        }
    }
}
