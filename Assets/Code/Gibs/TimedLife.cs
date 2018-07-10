using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Gibs
{
    public class TimedLife : MonoBehaviour
    {
        public float TimeAlive = 10f;
        private bool isDead = false;
        public bool FadeOut = true;
        private Color colorStart;
        private float alpha;
        public bool IsActived = true;
        private SpriteRenderer[] renders;

        void Start()
        {
            renders = GetComponentsInChildren<SpriteRenderer>();
            colorStart = renders.First().color;
            alpha = 1;
        }

        void Update()
        {
            if (IsActived)
            {
                if (isDead == false)
                {
                    TimeAlive -= Time.deltaTime;
                    if (TimeAlive <= 0)
                    {
                        isDead = true;
                        Destroy(gameObject);
                    }
                    if (FadeOut)
                    {
                        DoFadeOut();
                    }
                }
            }
        }

        private void DoFadeOut()
        {
            alpha -= 0.5f * Time.deltaTime;
            if (alpha < 0)
                alpha = 0;
            Color colorAlpha = new Color(colorStart.r, colorStart.g, colorStart.b, alpha);
            
            foreach(SpriteRenderer rend in renders)
            {
                rend.material.color = new Color(colorStart.r, colorStart.g, colorStart.b, alpha);
            }
        }
    }
}
