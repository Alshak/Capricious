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

        public bool FreezeMovementWhenHittingGround = false;
        private bool freezeMovement = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (this.tag == "Throwable") return;

            Collide(other);
        }

        private void Collide(Collider2D other)
        {
            if (!doNotKill)
            {
                //Evil Steve check
                if (this.tag == "Enemy" && other.tag == "Enemy")
                    return;

                //Gibs can only be destroyed by Throwables
                if (this.tag != "Throwable" && other.tag == "Gibs")
                    return;

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

                if (ShouldDieOnHit && other.tag != "Gibs" && other.tag != "Hurricane")
                {
                    Destroy(gameObject);
                }

                if (DeactivateOnFloor && this.GetComponent<Collider2D>().IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    doNotKill = true;
                }
            }
        }

        void Update()
        {
            if (FreezeMovementWhenHittingGround && freezeMovement == false)
            {
                if (GetComponent<Collider2D>().IsTouchingLayers(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Ground"))))
                {
                    freezeMovement = true;
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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

            var enemy = killed.GetComponent<EnemyController>();
            if (enemy != null)
            {
                return enemy.IsFacingRight;
            }
            return true;
        }
    }
}