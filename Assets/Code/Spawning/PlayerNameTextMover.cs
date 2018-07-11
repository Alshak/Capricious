using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets._2D;

namespace Assets.Code.Spawning
{
    public class PlayerNameTextMover : MonoBehaviour
    {
        private PlatformerCharacter2D playerCharacter;
        private PlayerName playerName;
        private LivesTextCounter textCounter;
        private PlayerLives lives;
        float cooldown = 0;
        float cooldownDisplay = 2f;


        void Start()
        {
            playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();
            lives = GameObject.FindObjectOfType<PlayerLives>();

            playerName = GetComponentInChildren<PlayerName>();
            textCounter = GetComponentInChildren<LivesTextCounter>();
        }

        void Update()
        {
            //Todo: Do stuff
            //this.transform.position = playerCharacter.transform.position;
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    Hide();
                }
            }
        }

        public void Reduce()
        {
            lives.Reduce();
        }

        public void UpdateLives()
        {
            textCounter.UpdateLives();
        }

        public void SetNewName()
        {
            playerName.SetNextSteveName();
            cooldown = cooldownDisplay;
        }

        public int GetLives()
        {
            return lives.GetLives();
        }

        private void Hide()
        {
            cooldown = 0;
            playerName.Hide();
            textCounter.Hide();
        }
    }
}
