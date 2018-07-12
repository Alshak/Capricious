using Assets.Code.Gibs;
using Assets.Code.Spawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets._2D;

namespace Assets.Code.Humanoids
{
    /// <summary>
    /// Killscript uses this to define if the object colliding with it can be killed.
    /// </summary>
    public class KillableByTraps : MonoBehaviour
    {
        public GameObject LeftDeathObj;
        public GameObject RightDeathObj;
        public float XOffset;
        public float YOffset;
        public bool IsDead = false;
        public bool IsKillableByEvilSteve = true;

        private CharacterSounds characterSounds;

        void Start()
        {
            characterSounds = GetComponentInChildren<CharacterSounds>();
            _characterInput = GetComponent<PlatformerCharacter2D>();
        }

        public bool IsPlayer = false;
        private PlatformerCharacter2D _characterInput;

        public virtual void Kill(bool isFacingRight)
        {
            if (IsDead)
                return;

            IsDead = true;
            GameObject createdObj = null;

            if(_characterInput != null)
                _characterInput.MoveParticles.Stop();

            if (isFacingRight)
            {
                if (RightDeathObj != null)
                {
                    var pos = transform.position;
                    RightDeathObj.transform.position = new Vector3(pos.x + XOffset, pos.y + YOffset, pos.z);
                    createdObj = Instantiate(RightDeathObj);
                }
            }
            else
            {
                if (LeftDeathObj != null)
                {
                    var pos = transform.position;
                    LeftDeathObj.transform.position = new Vector3(pos.x + XOffset, pos.y + YOffset, pos.z);
                    createdObj = Instantiate(LeftDeathObj);
                }
            }

            if (characterSounds != null)
            {
                characterSounds.PlayDeath();
            }

            if (IsPlayer)
            {
                PlayerRespawner respawner = GetComponent<PlayerRespawner>();
                if (respawner != null)
                {
                    var gibLife = createdObj.GetComponent<TimedLife>();
                    respawner.RespawnPlayer(gameObject, gibLife);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
