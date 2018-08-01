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
        public GameObject SpawnUponDeath;
        public float XOffset;
        public float YOffset;
        public bool IsDead = false;
        public bool IsKillableByEvilSteve = true;
        public bool IsPlayer = false;

        public GameObject zombieSoundPlayer;

        private PlatformerCharacter2D _characterInput;
        private CharacterSounds characterSounds;
        private Vector3 spawnUponDeathScale;
        private PlayerRespawner respawner;

        void Start()
        {
            characterSounds = GetComponentInChildren<CharacterSounds>();
            _characterInput = GetComponent<PlatformerCharacter2D>();

            if (IsPlayer)
            {
                respawner = GetComponent<PlayerRespawner>();
            }

            if (SpawnUponDeath != null)
            {
                spawnUponDeathScale = this.transform.localScale;
            }
        }


        public virtual void Kill(bool isFacingRight)
        {
            if (IsDead)
                return;

            IsDead = true;

            if(_characterInput != null)
            {
                _characterInput.MoveParticles.Stop();
            }

            TimedLife gibLife = null;
            if (SpawnUponDeath != null)
            {
                gibLife = SpawnGibs(isFacingRight);
                if (characterSounds != null)
                {
                    characterSounds.PlayDeath();
                }
            }

            if (zombieSoundPlayer != null)
            {
                var zombieSound = Instantiate(zombieSoundPlayer).GetComponent<CharacterSounds>();
                Debug.Log("zombieSound == null?" + zombieSound == null);
                zombieSound.PlayDeath();
            }

            if (IsPlayer)
            {
                respawner.RespawnPlayer(gameObject, gibLife);

            }
            else
            {
                Destroy(gameObject);
            }
        }

        private TimedLife SpawnGibs(bool isFacingRight)
        {
            GameObject createdObj = null;
            var pos = transform.position;
            SpawnUponDeath.transform.position = new Vector3(pos.x + XOffset, pos.y + YOffset, pos.z);

            if (isFacingRight)
            {
                SpawnUponDeath.transform.localScale = spawnUponDeathScale;
            }
            else
            {
                SpawnUponDeath.transform.position = new Vector3(pos.x + XOffset, pos.y + YOffset, pos.z);
                SpawnUponDeath.transform.localScale = new Vector3(spawnUponDeathScale.x * -1, spawnUponDeathScale.y, spawnUponDeathScale.z);
            }
            createdObj = Instantiate(SpawnUponDeath);


            if (IsPlayer)
            {
                if (respawner != null)
                {
                    return createdObj.GetComponent<TimedLife>();
                }
            }
            return null;
        }
    }
}
