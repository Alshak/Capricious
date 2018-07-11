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
        public GameObject LeftDeathObj;
        public GameObject RightDeathObj;
        public float XOffset;
        public float YOffset;
        public bool IsDead = false;
        public bool IsKillableByEvilSteve = true;

        public bool IsPlayer = false;
        public virtual void Kill(bool isFacingRight)
        {
            if (IsDead)
                return;

            IsDead = true;
            GameObject createdObj = null;

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
