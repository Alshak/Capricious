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

        private SpriteRenderer rend;

        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
            colorStart = rend.color;
        }

        void Update()
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

        private void DoFadeOut()
        {
            rend.material.color = new Color(colorStart.r, colorStart.g, colorStart.b);
        }
    }
}
