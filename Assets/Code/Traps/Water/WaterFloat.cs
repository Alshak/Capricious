using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps.Water
{
    public class WaterFloat : MonoBehaviour
    {
        public float waterLevel = -2f;
        public float floatHeight = 2;
        public float bounceDamp = 1f;
        public Vector3 buoyancyCentreOffset;
        private float forceFactor;
        private Vector3 actionPoint;
        private Vector3 uplift;

        void OnTriggerStay2D(Collider2D Hit)
        {
            if (Hit.tag == "Gibs" || Hit.tag == "Weapon")
            {
                actionPoint = Hit.transform.position + Hit.transform.TransformDirection(buoyancyCentreOffset);
                forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
                if (forceFactor > 0f)
                {
                    Rigidbody2D body = Hit.GetComponent<Rigidbody2D>();
                    uplift = -Physics.gravity * (forceFactor - body.velocity.y * bounceDamp);
                    body.AddForceAtPosition(uplift, actionPoint);
                }
            }
        }
    }
}
