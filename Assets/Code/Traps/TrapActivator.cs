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
            Traps = transform.parent.GetComponentsInChildren<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                foreach (Rigidbody2D trap in Traps)
                {
                    trap.isKinematic = false;
                }
            }
        }
    }
}
