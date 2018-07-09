using Assets.Code.Humanoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Killscript : MonoBehaviour
    {
        public bool ShouldDieOnHit = false;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var killable = other.GetComponent<KillableByTraps>();
            if (killable != null)
            {
                killable.Kill();
            }

            if (ShouldDieOnHit)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var killable = other.collider.GetComponent<KillableByTraps>();
            if (killable != null)
            {
                killable.Kill();
            }

            if (ShouldDieOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
