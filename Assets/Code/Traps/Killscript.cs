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
        [SerializeField] private bool ShouldDieOnHit = false;
        [SerializeField] private bool DeactivateOnFloor = false;
        [SerializeField] private bool UseTrigger = false;
        private bool doNotKill = false;
        private void OnTriggerEnter2D(Collider2D other)
        {            
            if (UseTrigger && !doNotKill)
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

                if (DeactivateOnFloor && other.IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    doNotKill = true;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!UseTrigger && !doNotKill)
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

                if (DeactivateOnFloor && other.collider.IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    doNotKill = true;
                }
            }
        }
    }
}
