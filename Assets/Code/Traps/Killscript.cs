using Assets.Code.Humanoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets._2D;

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
                    killable.Kill(IsFacingRight(killable));
                }

                if (ShouldDieOnHit && other.tag != "Gibs")
                {
                    Destroy(gameObject);
                }

                if (DeactivateOnFloor && other.IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    //doNotKill = true;
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
                    killable.Kill(IsFacingRight(killable));
                }

                if (ShouldDieOnHit && other.collider.tag != "Gibs")
                {
                    Destroy(gameObject);
                }

                var ourCollider = GetComponent<Collider2D>();
                //Debug.Log("is hitting floor: " + ourCollider.IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))));
                if (DeactivateOnFloor && ourCollider.IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    //doNotKill = true;
                }
            }
        }

        private bool IsFacingRight(KillableByTraps killed)
        {
            var character = killed.GetComponent<PlatformerCharacter2D>();
            if (character != null)
            {
                return character.IsFacingRight();
            }
            return true;
        }
    }
}