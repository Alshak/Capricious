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
        private Platformer2DUserControl playerCharacter;
        private PlayerName playerName;
        private LivesTextCounter textCounter;
        private PlayerLives lives;
        float cooldown = 0;
        float cooldownDisplay = 2.5f;
        public float timeBeforeFade = 1f;

        private Vector3 playerPos;
        public float XOffset = -0.4f;
        public float YOffset = 2.13f;
        private float alpha = 1;

        void Start()
        {
            playerCharacter = GameObject.FindObjectOfType<Platformer2DUserControl>();
            lives = GameObject.FindObjectOfType<PlayerLives>();

            playerName = GetComponentInChildren<PlayerName>();
            textCounter = GetComponentInChildren<LivesTextCounter>();
            cooldown = 5f;
            this.transform.SetParent(null);
        }

        void Update()
        {
            //Todo: Do stuff
            //this.transform.position = playerCharacter.transform.position;
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                if (cooldown < timeBeforeFade)
                    Fadeout();
                if (cooldown <= 0)
                {
                    Hide();
                }
            }
            Vector3 playerPos = playerCharacter.transform.position;
            this.transform.position = new Vector3(playerPos.x + XOffset, playerPos.y + YOffset, this.transform.position.z);
            
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
            alpha = 1;
            playerName.Fadeout(alpha);
            textCounter.Fadeout(alpha);
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

        private void Fadeout()
        {
            alpha -= 0.05f;
            if (alpha <= 0)
                alpha = 0;

            playerName.Fadeout(alpha);
            textCounter.Fadeout(alpha);
        }
    }
}
