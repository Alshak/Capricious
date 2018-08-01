using Assets.Code.ChaseLevel;
using Assets.Code.Gibs;
using Assets.Code.Humanoids;
using Assets.Code.LevelChange;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

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

        private float cooldownDeath = 0;
        private bool gameOver = false;

        private ResetManager resetManager;

        //private PlayerLives lives;
        //private LivesTextCounter livesCounter;
        private MusicManager musicManager;
        private PlayerNameTextMover playerNameTextMover;

        private AudioSource respawnSound;

        void Start()
        {
            respawnSound = GetComponent<AudioSource>();
            playerNameTextMover = GameObject.FindObjectOfType<PlayerNameTextMover>();
            musicManager = GameObject.FindObjectOfType<MusicManager>();
            resetManager = GameObject.FindObjectOfType<ResetManager>();
        }

        void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    if (playerNameTextMover.GetLives() > 0)
                    {
                        SpawnPlayer();

                    }
                    else
                    {
                        GameOver();
                    }
                }
            }

            if (gameOver && cooldownDeath > 0)
            {
                cooldownDeath -= Time.deltaTime;
                if (cooldownDeath <= 0)
                {
                    Debug.Log("RESPAWN PLS");
                    LoadMainMenu();
                }
            }
        }

        public void RespawnPlayer(GameObject player, TimedLife newTimedLife)
        {
            if (cooldown > 0)
                return;
            cooldown = WaitTimePerDeath;
            player.GetComponent<Platformer2DUserControl>().IsAlive = false;
            player.GetComponent<Rigidbody2D>().simulated = false;
            this.player = player;
            player.GetComponent<Animator>().enabled = false;
            player.GetComponent<PlatformerCharacter2D>().ResetEverything();

            SpriteRenderer[] renderers = player.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            if (PrevTimeLife != null)
            {
                PrevTimeLife.IsActived = true;
            }
            PrevTimeLife = newTimedLife;
            playerNameTextMover.Reduce();

            if (resetManager != null)
            {
                resetManager.ResetLevel();
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

        private void SpawnPlayer()
        {
            //player.GetComponent
            playerNameTextMover.SetNewName();
            playerNameTextMover.UpdateLives();
            cooldown = 0;
            respawnSound.Play();
            player.GetComponent<KillableByTraps>().IsDead = false;
            player.GetComponent<Platformer2DUserControl>().IsAlive = true;
            player.GetComponent<Animator>().enabled = true;
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<PlatformerCharacter2D>().PlaySpawnSound = true;
            player.transform.position = CurrentSpawn.transform.position;
            Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
            rigid.simulated = true;

            if (rigid != null)
            {
                rigid.transform.position = CurrentSpawn.transform.position;
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = 0f;
            }
        }

        public void GameOver()
        {
            cooldown = 0;
            cooldownDeath = 2f;
            gameOver = true;
            musicManager.PlayDeath();
        }

        private void LoadMainMenu()
        {
            gameOver = false;
            cooldown = 0;
            cooldownDeath = 0;
            SceneManager.LoadScene(LevelName.Game_End.ToString(), LoadSceneMode.Single);
        }
    }
}
