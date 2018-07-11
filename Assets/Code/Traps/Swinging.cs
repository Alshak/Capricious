using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Swinging : MonoBehaviour
    {
        private Quaternion startPos;
        public bool IsActivated = true;
        float timerSinceStart;
        public float Speed = 1f;
        void Start()
        {
            startPos = transform.rotation;
            timerSinceStart = 0f;
        }

        void Update()
        {
            timerSinceStart += Time.deltaTime * Speed;
            if (Time.timeScale > 0)
            {
                transform.Rotate(new Vector3(0, 0, 1.5f * Speed * Mathf.Sin(timerSinceStart)));
            }
        }
    }
}
