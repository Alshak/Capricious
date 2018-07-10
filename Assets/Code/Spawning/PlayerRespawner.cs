using Assets.Code.Gibs;
using Assets.Code.Humanoids;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class PlayerRespawner : MonoBehaviour
    {
        public Spawnpoint CurrentSpawn;
        private Checkpoint currentCheckpoint;
        private TimedLife PrevTimeLife;
        public float WaitTimePerDeath = 3f;
        private float cooldown = 0;
        private GameObject player;

        void Start()
        {
        }

        void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    SpawnPlayer();
                }
            }
        }

        public void RespawnPlayer(GameObject player, TimedLife newTimedLife)
        {
            if (cooldown > 0)
                return;
            cooldown = WaitTimePerDeath;
            this.player = player;

            player.GetComponent<SpriteRenderer>().enabled = false;

            if (PrevTimeLife != null)
            {
                Debug.Log("prev life null");
                PrevTimeLife.IsActived = true;
            }
            PrevTimeLife = newTimedLife;
        }

        public void SetRespawn(Checkpoint checkpoint)
        {
            if (currentCheckpoint != null)
            {
                currentCheckpoint.SetActived(false);
            }
            currentCheckpoint = checkpoint;
            CurrentSpawn = currentCheckpoint.GetSpawnpoint();
        }

        private void SpawnPlayer()
        {
            //player.GetComponent
            cooldown = 0;
            player.GetComponent<KillableByTraps>().IsDead = false;
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.transform.position = CurrentSpawn.transform.position;
            Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();

            if (rigid != null)
            {
                rigid.transform.position = CurrentSpawn.transform.position;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = 0f;
            }
            currentCheckpoint.SetNextSteveName();
        }
    }
}
