using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Intro
{
    public class ImageBlinking : MonoBehaviour
    {
        private bool fadeIn = true;
        private float alpha;
        private Image rend;
        public float Speed = 0.1f;

        void Start()
        {
            rend = GetComponent<Image>();
        }

        private void Update()
        {
            if (fadeIn)
            {
                alpha += Speed;
                if (alpha >= 1)
                {
                    alpha = 1;
                    fadeIn = false;
                }
            }
            else
            {
                alpha -= Speed;
                if (alpha <= 0)
                {
                    alpha = 0;
                    fadeIn = true;
                }
            }

            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
        }
    }
}
