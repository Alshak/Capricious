using Assets.Code.Gibs;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class PlayerRespawner : MonoBehaviour
    {
        public Spawnpoint CurrentSpawn;
        private Checkpoint currentCheckpoint;
        private TimedLife PrevTimeLife;

        public void RespawnPlayer(GameObject player, TimedLife newTimedLife)
        {
            player.transform.position = CurrentSpawn.transform.position;
            Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();

            if (PrevTimeLife != null)
            {
                Debug.Log("prev life null");
                PrevTimeLife.IsActived = true;
            }
            PrevTimeLife = newTimedLife;

            if (rigid != null)
            {
                rigid.transform.position = CurrentSpawn.transform.position;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = 0f;
            }
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
    }
}
