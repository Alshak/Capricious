using Assets.Code.Gibs;
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
        public float XOffset;
        public float YOffset;

        public bool IsPlayer = false;
        public virtual void Kill()
        {
            GameObject createdObj = null;
            if (CreateUponDeath != null)
            {
                var pos = transform.position;
                CreateUponDeath.transform.position = new Vector3(pos.x + XOffset, pos.y + YOffset, pos.z);
                createdObj = Instantiate(CreateUponDeath);
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
