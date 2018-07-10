using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class Checkpoint : MonoBehaviour
    {
        public Color ColorDefault;
        public Color ColorActivated;

        private Spawnpoint spawnpoint;

        private SpriteRenderer rend;
        public bool IsActived = false;
        
        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
            spawnpoint = GetComponentInChildren<Spawnpoint>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && IsActived == false)
            {
                PlayerRespawner respawner = other.GetComponent<PlayerRespawner>();
                if (respawner != null)
                {
                    SetActived(true);
                    respawner.SetRespawn(this);
                }
            }
        }

        public void SetActived(bool active)
        {
            IsActived = active;
            //rend.material.color = IsActived ? ColorActivated : ColorDefault;
        }

        public Spawnpoint GetSpawnpoint()
        {
            return spawnpoint;
        }
    }
}
