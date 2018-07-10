using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class PlayerRespawner : MonoBehaviour
    {
        public Spawnpoint CurrentSpawn;

        public void RespawnPlayer(GameObject player)
        {
            Debug.Log("RESPAWN!");
            player.transform.position = CurrentSpawn.transform.position;
            Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();

            if (rigid != null)
            {
                rigid.transform.position = CurrentSpawn.transform.position;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = 0f;
            }
        }
    }
}
