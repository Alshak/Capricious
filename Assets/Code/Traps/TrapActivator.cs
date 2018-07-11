using Assets.Code.Gibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class TrapActivator : MonoBehaviour
    {
        private bool isActivated = false;
        private bool isDone = false;
        private List<Rigidbody2D> Traps;
        
        
        [Range(0, 1f)]
        public float DelayPerDrop = 0.2f;
        private float cooldown = 0f;
        private int trapIndex = 0;

        void Start()
        {
            Traps = transform.parent.GetComponentsInChildren<Rigidbody2D>().ToList();
        }

        void Update()
        {
            if (isActivated && isDone == false)
            {
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown <= 0)
                    {
                        cooldown = DelayPerDrop;
                        UnFreezeOne();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && isActivated == false && isDone == false)
            {
                isActivated = true;
                if (DelayPerDrop == 0)
                {
                    UnFreezeAll();
                }
                cooldown = DelayPerDrop;
            }
        }

        private void UnFreezeOne()
        {
            Rigidbody2D trap = Traps[trapIndex];
            trap.isKinematic = false;
            var timedLife = trap.GetComponent<TimedLife>();
            if (timedLife != null)
            {
                timedLife.IsActived = true;
            }
            trapIndex++;
            if (trapIndex >= Traps.Count)
            {
                isDone = true;
            }
        }

        private void UnFreezeAll()
        {
            foreach (Rigidbody2D trap in Traps)
            {
                trap.isKinematic = false;
                var timedLife = trap.GetComponent<TimedLife>();
                if (timedLife != null)
                {
                    timedLife.IsActived = true;
                }
            }
        }
    }
}
