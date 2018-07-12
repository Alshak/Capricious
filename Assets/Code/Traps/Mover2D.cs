using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Mover2D : MonoBehaviour
    {
        [Range(-10, 10f)]
        public float SpeedX;

        [Range(-10, 10f)]
        public float SpeedY;

        public bool IsActivated = true;

        void Update()
        {
            if (IsActivated)
            {
                Vector3 pos = transform.position;
                transform.position = new Vector3(pos.x + SpeedX * Time.deltaTime, pos.y + SpeedY * Time.deltaTime, pos.z);
            }
        }
    }
}
