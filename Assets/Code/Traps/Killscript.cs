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
        private bool doNotKill = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Collide(other);
        }

        private void Collide(Collider2D other)
        {
            if (!doNotKill)
            {
                var killable = other.GetComponent<KillableByTraps>();
                if (killable != null)
                {
                    killable.Kill(IsFacingRight(killable));
                }
                else
                {
                    if (other.transform.parent != null)
                    {
                        killable = other.transform.parent.GetComponent<KillableByTraps>();
                        if (killable != null)
                        {
                            killable.Kill(IsFacingRight(killable));
                        }
                    }
                }

                if (ShouldDieOnHit && other.tag != "Gibs")
                {
                    Destroy(gameObject);
                }

                if (DeactivateOnFloor && this.GetComponent<Collider2D>().IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    doNotKill = true;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Collide(other.collider);
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