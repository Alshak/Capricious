using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Intro
{
    public class ControlButton : MonoBehaviour
    {
        public ControlKey key;
        private ControlButtonImages images;
        private Image img;

        void Start()
        {
            images = GetComponentInParent<ControlButtonImages>();
            img = GetComponent<Image>();
            //var asdf  = Input.GetJoystickNames
            //Debug.Log("adsadasd : " + asdf);
        }


        public void RefreshKeyImage(bool controller)
        {
            if (controller)
            {
                switch (key)
                {
                    case ControlKey.Jump:
                        break;
                }
            }
        }

        private void SetKeyImg(int button)
        {
            switch (button)
            {
                case 0:
                    //A
                    img.sprite = images.ControllerA;
                    break;
                case 1:
                    //B
                    img.sprite = images.ControllerA;
                    break;
                case 2:
                    //X
                    img.sprite = images.ControllerA;
                    break;
                case 3:
                    //Y
                    img.sprite = images.ControllerA;
                    break;
            }
        }
    }

    public enum ControlKey
    {
        Jump,
        Slide,
        Throw
    }
}
