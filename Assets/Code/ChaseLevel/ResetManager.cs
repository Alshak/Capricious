using Assets.Code.LevelChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.ChaseLevel
{
    public class ResetManager :  MonoBehaviour
    {
        public LevelName CurrentLevel;
        public float cooldownWait = 3f;
        private float cooldown = 0f;
        private bool isActivated = false;

        void Update()
        {
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    RestartLevel();
                }
            }
        }

        public void ResetLevel()
        {
            cooldown = cooldownWait;
        }

        private void RestartLevel()
        {
            if (isActivated == false)
            {
                isActivated = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
