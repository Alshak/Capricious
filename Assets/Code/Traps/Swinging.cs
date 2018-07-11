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
        float minZ = 999;
        float maxZ = -999;
        bool clockwise = false;
        void Start()
        {
            startPos = transform.rotation;
            timerSinceStart = 0f;
        }

        void Update()
        {
            if(clockwise && transform.rotation.z > 0.99f)
            {
                clockwise = false;
            }else if(!clockwise && transform.rotation.z < 0)
            {
                clockwise = true;
            }

            if (Time.timeScale > 0)
            {
                if (!clockwise)
                {
                    transform.Rotate(new Vector3(0, 0, Speed));
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, -Speed));
                }
            }
            minZ = Mathf.Min(minZ, transform.rotation.z);
            maxZ = Mathf.Max(maxZ, transform.rotation.z);
            Debug.Log(transform.rotation.z);
        }
    }
}
