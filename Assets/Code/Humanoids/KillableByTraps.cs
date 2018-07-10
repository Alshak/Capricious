using Assets.Code.Spawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    /// <summary>
    /// Killscript uses this to define if the object colliding with it can be killed.
    /// </summary>
    public class KillableByTraps : MonoBehaviour
    {
        public GameObject CreateUponDeath;
        public bool IsPlayer = false;
        public virtual void Kill()
        {
            if (CreateUponDeath != null)
            {
                CreateUponDeath.transform.position = transform.position;
                Instantiate(CreateUponDeath);
            }

            if (IsPlayer)
            {
                PlayerRespawner respawner = GetComponent<PlayerRespawner>();
                if (respawner != null)
                {
                    respawner.RespawnPlayer(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
