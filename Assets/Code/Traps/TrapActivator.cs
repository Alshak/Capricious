using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class TrapActivator : MonoBehaviour
    {
        private Rigidbody2D[] Traps;

        void Start()
        {
            Traps = GetComponentsInChildren<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach(Rigidbody2D trap in Traps)
            {
                trap.WakeUp();
            }
        }
    }
}
